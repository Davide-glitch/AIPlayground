import {
  Box,
  IconButton,
  Paper,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography,
  Button,
  Chip,
} from "@mui/material";
import { FC, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./Prompts.css";
import { Delete, Message, Add } from "@mui/icons-material";
import { EmptyTableRow } from "../common/EmptyTableRow";
import { LoadingRow } from "../common/LoadingRow";
import { TableHeader } from "../common/TableHeader";
import { Prompt } from "../shared/types/Prompt";
import { PromptsApiClient } from "../../api/Clients/PromptsApiClient";
import { PromptModel } from "../../api/Models/PromptModel";
import { DeletePopup } from "../common/DeletePopup";
import { TruncatedText } from "../common/TruncatedText";

export const Prompts: FC = () => {
  const [prompts, setPrompts] = useState<Prompt[]>([]);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [openDeletePopup, setOpenDeletePopup] = useState<boolean>(false);
  const [promptToDelete, setPromptToDelete] = useState<Prompt>();
  const navigate = useNavigate();

  const columns = [
    {
      id: "id",
      label: "Id",
      className: "prompts-id-column",
    },
    {
      id: "name",
      label: "Name",
      className: "prompts-name-column",
    },
    {
      id: "systemMessage",
      label: "System message",
      className: "prompts-system-column",
    },
    {
      id: "userMessage",
      label: "User message",
      className: "prompts-user-column",
    },
    {
      id: "expectedResult",
      label: "Expected result",
      className: "prompts-expected-column",
    },
    {
      id: "actions",
      label: "Actions",
      className: "prompts-actions-column",
    },
  ];

  const fetchPrompts = async () => {
    try {
      setIsLoading(true);

      const res = await PromptsApiClient.getAllAsync();

      const fetchedPrompts = res.map((e: PromptModel) => ({ ...e } as Prompt));

      setPrompts(fetchedPrompts);

      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  const deletePrompt = async (promptToDelete: Prompt) => {
    try {
      if (!promptToDelete.id) {
        return;
      }

      await PromptsApiClient.deleteOneAsync(promptToDelete.id);

      setPrompts(prompts.filter((p) => p.id !== promptToDelete.id));
    } catch (error: any) {
      console.log(error);
    }
  };

  const handleCreatePrompt = () => {
    navigate("/prompts/create");
  };

  const renderActions = (prompt: Prompt) => {
    return (
      <IconButton
        onClick={() => {
          setPromptToDelete(prompt);
          setOpenDeletePopup(true);
        }}
      >
        <Delete color="primary" fontSize="large" />
      </IconButton>
    );
  };

  useEffect(() => {
    fetchPrompts();
  }, []);

  return (
    <Box className="prompts-wrapper">
      {/* Enhanced Header */}
      <Box className="prompts-header">
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="center"
          spacing={2}
          mb={2}
        >
          <Message sx={{ fontSize: 40, color: "#8b5cf6" }} />
          <Typography variant="h2" className="prompts-title">
            Prompts
          </Typography>
        </Stack>
        <Typography variant="body1" className="prompts-subtitle">
          Create and manage intelligent prompts for different AI models and use
          cases
        </Typography>
        <Stack
          direction="row"
          spacing={2}
          justifyContent="center"
          alignItems="center"
        >
          <Chip
            label={`${prompts.length} Prompts Available`}
            color="primary"
            sx={{ fontWeight: 600 }}
          />
          <Button
            variant="contained"
            startIcon={<Add />}
            onClick={handleCreatePrompt}
            className="prompts-add-button"
          >
            Create New Prompt
          </Button>
        </Stack>
      </Box>

      <TableContainer
        component={Paper}
        className="prompts-table-container enhanced-table-container"
      >
        <Table>
          <TableHeader columns={columns} />
          <TableBody>
            {prompts && prompts.length ? (
              <>
                {prompts.map((prompt: Prompt, index: number) => (
                  <TableRow
                    key={index}
                    className={"prompts-table-row"}
                    onDoubleClick={() =>
                      navigate(`/prompts/view/${prompt.id}`)
                    }
                    sx={{
                      cursor: "pointer",
                    }}
                  >
                    <TableCell align="center" /> {/* Empty cell to shift content */}
                    <TableCell align="center" className="prompts-id-column">{prompt.id}</TableCell>
                    <TableCell align="center" className="prompts-name-column">
                      <Typography
                        variant="subtitle1"
                        sx={{
                          fontWeight: 600,
                          color: "#1e293b",
                        }}
                      >
                        <TruncatedText
                          text={prompt.name || ""}
                          maxLength={20}
                        />
                      </Typography>
                    </TableCell>
                    <TableCell align="center" className="prompts-system-column truncated-cell">
                      <TruncatedText
                        text={prompt.systemMessage || ""}
                        maxLength={25}
                      />
                    </TableCell>
                    <TableCell align="center" className="prompts-user-column truncated-cell">
                      <TruncatedText
                        text={prompt.userMessage || ""}
                        maxLength={25}
                      />
                    </TableCell>
                    <TableCell align="center" className="prompts-expected-column truncated-cell">
                      <TruncatedText
                        text={prompt.expectedResult || ""}
                        maxLength={25}
                      />
                    </TableCell>
                    <TableCell align="center" className="prompts-actions-column">
                      {renderActions(prompt)}
                    </TableCell>
                  </TableRow>
                ))}
              </>
            ) : isLoading ? (
              <LoadingRow />
            ) : (
              <EmptyTableRow />
            )}
          </TableBody>
        </Table>
        <Box className={"prompts-table-footer"}>
          {`${prompts.length} prompts`}
        </Box>
      </TableContainer>

      {/* Existing DeletePopup */}
      <DeletePopup
        entityTitle={promptToDelete?.name ?? "Unknown"}
        open={openDeletePopup}
        onClose={() => {
          setOpenDeletePopup(false);
          setPromptToDelete(undefined);
        }}
        onConfirm={() => {
          if (promptToDelete) {
            deletePrompt(promptToDelete);
          }
          setOpenDeletePopup(false);
          setPromptToDelete(undefined);
        }}
      />
    </Box>
  );
};
