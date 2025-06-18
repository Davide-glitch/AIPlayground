import { FC, useEffect, useState } from "react";
import "./Runs.css";
import { RunsApiClient } from "../../api/Clients/RunsApiClient";
import {
  Box,
  Button,
  IconButton,
  Paper,
  Popover,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  TextField,
  Typography,
  Chip,
} from "@mui/material";
import { Star, PlayCircle, TrendingUp, Delete } from "@mui/icons-material";
import { EmptyTableRow } from "../common/EmptyTableRow";
import { LoadingRow } from "../common/LoadingRow";
import { TableHeader } from "../common/TableHeader";
import { RunGet } from "../shared/types/RunGet";
import { RunGetModel } from "../../api/Models/RunGetModel";
import { TruncatedText } from "../common/TruncatedText";

export const Runs: FC = () => {
  const [runs, setRuns] = useState<RunGet[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const [ratingValue, setRatingValue] = useState<number | "">("");
  const [selectedRunId, setSelectedRunId] = useState<number | null>(null);
  const [deleteAnchorEl, setDeleteAnchorEl] = useState<null | HTMLElement>(null);
  const [deleteRunId, setDeleteRunId] = useState<string>("");
  const [selectedDeleteRunId, setSelectedDeleteRunId] = useState<number | null>(null);

  const columns = [
    {
      id: "id",
      label: "Id",
      className: "runs-id-column",
    },
    {
      id: "promptName",
      label: "Prompt name",
      className: "runs-prompt-column",
    },
    {
      id: "expectedResult",
      label: "Expected result",
      className: "runs-expected-column",
    },
    {
      id: "actualResult",
      label: "Actual result",
      className: "runs-actual-column",
    },
    {
      id: "model",
      label: "Model",
      className: "runs-model-column",
    },
    {
      id: "rating",
      label: "Rating",
      className: "runs-rating-column",
    },
    {
      id: "userRating",
      label: "User rating",
      className: "runs-user-rating-column",
    },
    {
      id: "actions",
      label: "Actions",
      className: "runs-actions-column",
    },
  ];

  const fetchRuns = async () => {
    try {
      setIsLoading(true);

      const res = await RunsApiClient.getAllAsync();

      const fetchedRuns = res.map(
        (e: RunGetModel) =>
          ({
            id: e.id,
            promptName: e.prompt.name,
            expectedResult: e.prompt.expectedResult,
            actualResult: e.actualResponse,
            model: e.model.name,
            rating: e.rating,
            userRating: e.userRating,
          } as RunGet)
      );

      setRuns(fetchedRuns);

      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchRuns();
  }, []);

  const renderActions = (run: RunGet) => {
    const open = Boolean(anchorEl);
    const deleteOpen = Boolean(deleteAnchorEl);

    const handleOpen = (
      event: React.MouseEvent<HTMLElement>,
      runId: number
    ) => {
      setAnchorEl(event.currentTarget);
      setSelectedRunId(runId);
    };

    const handleClose = () => {
      setAnchorEl(null);
      setRatingValue("");
      setSelectedRunId(null);
    };

    const handleDeleteOpen = (
      event: React.MouseEvent<HTMLElement>,
      runId: number
    ) => {
      setDeleteAnchorEl(event.currentTarget);
      setSelectedDeleteRunId(runId);
    };

    const handleDeleteClose = () => {
      setDeleteAnchorEl(null);
      setDeleteRunId("");
      setSelectedDeleteRunId(null);
    };

    const handleGiveRating = async () => {
      if (
        selectedRunId != null &&
        ratingValue !== "" &&
        ratingValue >= 0 &&
        ratingValue <= 100
      ) {
        await RunsApiClient.rateAsync(selectedRunId, ratingValue);
        await fetchRuns();
        handleClose();
      }
    };

    const handleDeleteRun = async () => {
      if (selectedDeleteRunId != null && deleteRunId === selectedDeleteRunId.toString()) {
        try {
          await RunsApiClient.deleteAsync(selectedDeleteRunId);
          await fetchRuns();
          handleDeleteClose();
        } catch (error) {
          console.error("Error deleting run:", error);
        }
      }
    };

    return (
      <>
        <Stack direction="row" spacing={1}>
          <IconButton onClick={(e) => handleOpen(e, run.id)}>
            <Star color="primary" fontSize="large" />
          </IconButton>
          <IconButton 
            onClick={(e) => handleDeleteOpen(e, run.id)}
            sx={{ color: "#ef4444" }}
          >
            <Delete fontSize="large" />
          </IconButton>
        </Stack>
        
        {/* Rating Popover */}
        <Popover
          open={open && selectedRunId === run.id}
          anchorEl={anchorEl}
          onClose={handleClose}
          anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
          transformOrigin={{ vertical: "top", horizontal: "center" }}
        >
          <Box
            sx={{
              p: 2,
              display: "flex",
              flexDirection: "column",
              gap: 1,
              width: 300,
            }}
          >
            <TextField
              label="Rating (0-100)"
              type="number"
              fullWidth
              value={ratingValue}
              onChange={(e) => setRatingValue(Number(e.target.value))}
              slotProps={{
                htmlInput: {
                  min: 0,
                  max: 100,
                },
              }}
            />
            <Button
              onClick={handleGiveRating}
              disabled={
                ratingValue === "" || ratingValue < 0 || ratingValue > 100
              }
            >
              Submit
            </Button>
          </Box>
        </Popover>

        {/* Delete Popover */}
        <Popover
          open={deleteOpen && selectedDeleteRunId === run.id}
          anchorEl={deleteAnchorEl}
          onClose={handleDeleteClose}
          anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
          transformOrigin={{ vertical: "top", horizontal: "center" }}
        >
          <Box
            sx={{
              p: 2,
              display: "flex",
              flexDirection: "column",
              gap: 1,
              width: 300,
            }}
          >
            <Typography variant="h6" color="error">
              Delete Run
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Please enter the run ID to confirm deletion:
            </Typography>
            <TextField
              label="Run ID"
              type="text"
              fullWidth
              value={deleteRunId}
              onChange={(e) => setDeleteRunId(e.target.value)}
              placeholder={selectedDeleteRunId?.toString() || ""}
            />
            <Button
              variant="contained"
              color="error"
              startIcon={<Delete />}
              onClick={handleDeleteRun}
              disabled={deleteRunId !== selectedDeleteRunId?.toString()}
            >
              Delete Run
            </Button>
          </Box>
        </Popover>
      </>
    );
  };

  return (
    <Box className="runs-wrapper">
      {/* Enhanced Header */}
      <Box className="runs-header">
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="center"
          spacing={2}
          mb={2}
        >
          <PlayCircle sx={{ fontSize: 40, color: "#ec4899" }} />
          <Typography variant="h2" className="runs-title">
            Runs
          </Typography>
        </Stack>
        <Typography variant="body1" className="runs-subtitle">
          Track and analyze your AI model runs with detailed performance metrics
          and ratings
        </Typography>
        <Stack
          direction="row"
          spacing={2}
          justifyContent="center"
          alignItems="center"
          mt={2}
        >
          <Chip
            label={`${runs.length} Total Runs`}
            color="primary"
            icon={<TrendingUp />}
            sx={{ fontWeight: 600 }}
          />
          <Chip
            label={`${runs.filter((r) => r.userRating > 4).length} High Rated`}
            color="success"
            sx={{ fontWeight: 600 }}
          />
        </Stack>
      </Box>

      <TableContainer
        component={Paper}
        className="runs-table-container enhanced-table-container"
      >
        <Table>
          <TableHeader columns={columns} />
          <TableBody>
            {isLoading ? (
              <LoadingRow />
            ) : runs.length === 0 ? (
              <EmptyTableRow />
            ) : (
              runs.map((run: RunGet, index: number) => (
                <TableRow
                  key={index}
                  className="runs-table-row"
                >
                  <TableCell align="center" /> {/* Empty cell to shift content */}
                  <TableCell align="center" className="runs-id-column">{run.id}</TableCell>
                  <TableCell align="center" className="runs-prompt-column">
                    <Typography
                      variant="subtitle1"
                      sx={{
                        fontWeight: 600,
                        color: "#1e293b",
                      }}
                    >
                      <TruncatedText
                        text={run.promptName || ""}
                        maxLength={30}
                      />
                    </Typography>
                  </TableCell>
                  <TableCell align="center" className="runs-expected-column truncated-cell">
                    <TruncatedText
                      text={run.expectedResult || ""}
                      maxLength={40}
                    />
                  </TableCell>
                  <TableCell align="center" className="runs-actual-column truncated-cell">
                    <TruncatedText
                      text={run.actualResult || ""}
                      maxLength={40}
                    />
                  </TableCell>
                  <TableCell align="center" className="runs-model-column">
                    <Chip
                      label={run.model}
                      className="runs-model-chip"
                    />
                  </TableCell>
                  <TableCell align="center" className="runs-rating-column">
                    {run.rating}
                  </TableCell>
                  <TableCell align="center" className="runs-user-rating-column">
                    {run.userRating}
                  </TableCell>
                  <TableCell align="center" className="runs-actions-column">
                    {renderActions(run)}
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
        <Box className="runs-table-footer">
          {`${runs.length} runs`}
        </Box>
      </TableContainer>
    </Box>
  );
};
