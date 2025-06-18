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
import { TableHeader } from "../common/TableHeader";
import { LoadingRow } from "../common/LoadingRow";
import { EmptyTableRow } from "../common/EmptyTableRow";
import EditIcon from "@mui/icons-material/Edit";
import { AddScopePopup } from "./AddScopePopup";
import { EditScopePopup } from "./EditScopePopup";
import DeleteIcon from "@mui/icons-material/Delete";
import "./Scopes.css";
import { Scope } from "../shared/types/Scope";
import { ScopesApiClient } from "../../api/Clients/ScopesApiClient";
import { ScopeModel } from "../../api/Models/ScopeModel";
import { DeletePopup } from "../common/DeletePopup";
import { Lightbulb, Add } from "@mui/icons-material";
import { TruncatedText } from "../common/TruncatedText";

export const Scopes: FC = () => {
  const [scopes, setScopes] = useState<Scope[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const [openAddPopup, setOpenAddPopup] = useState(false);
  const handleOpenAddPopup = () => setOpenAddPopup(true);
  const handleCloseAddPopup = () => setOpenAddPopup(false);

  const [editableScope, setEditableScope] = useState<Scope>();
  const [openEditPopup, setOpenEditPopup] = useState(false);
  const handleCloseEditPopup = () => setOpenEditPopup(false);

  const [scopeToDelete, setScopeToDelete] = useState<Scope>();
  const [openDeletePopup, setOpenDeletePopup] = useState(false);

  const columns = [
    {
      id: "id",
      label: "Id",
      className: "scopes-id-column",
    },
    {
      id: "name",
      label: "Name",
      className: "scopes-name-column",
    },
    {
      id: "actions",
      label: "Actions",
      className: "scopes-actions-column",
    },
  ];

  const fetchScopes = async () => {
    try {
      setIsLoading(true);

      const res = await ScopesApiClient.getAllAsync();

      const fetchedScopes = res.map((e: ScopeModel) => ({ ...e } as Scope));

      setScopes(fetchedScopes);

      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  const deleteScope = async (deletedScope: Scope) => {
    try {
      if (!deletedScope.id) {
        return;
      }

      const res = await ScopesApiClient.deleteOneAsync(deletedScope.id);

      setScopes(scopes.filter((scope) => scope.id !== deletedScope.id));
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchScopes();
  }, []);

  return (
    <Box className="scopes-wrapper">
      {/* Enhanced Header */}
      <Box className="scopes-header">
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="center"
          spacing={2}
          mb={2}
        >
          <Lightbulb sx={{ fontSize: 40, color: "#6366f1" }} />
          <Typography variant="h2" className="scopes-title">
            Scopes
          </Typography>
        </Stack>
        <Typography variant="body1" className="scopes-subtitle">
          Organize your AI prompts into different categories and contexts for
          better management
        </Typography>
        <Stack
          direction="row"
          spacing={2}
          justifyContent="center"
          alignItems="center"
        >
          <Chip
            label={`${scopes.length} Scopes Active`}
            color="primary"
            sx={{ fontWeight: 600 }}
          />
          <Button
            variant="contained"
            startIcon={<Add />}
            onClick={() => setOpenAddPopup(true)}
            className="scopes-add-button"
          >
            Add New Scope
          </Button>
        </Stack>
      </Box>

      <TableContainer
        component={Paper}
        className="scopes-table-container enhanced-table-container"
      >
        <Table>
          <TableHeader columns={columns} />
          <TableBody>
            {isLoading ? (
              <LoadingRow />
            ) : scopes.length === 0 ? (
              <EmptyTableRow />
            ) : (
              scopes.map((scope) => (
                <TableRow key={scope.id} className="scopes-table-row">
                  <TableCell align="center" /> {/* Empty cell to shift content */}
                  <TableCell align="center" className="scopes-id-column">{scope.id}</TableCell>
                  <TableCell align="center" className="scopes-name-column">
                    <Typography
                      variant="subtitle1"
                      sx={{
                        fontWeight: 600,
                        color: "#1e293b",
                      }}
                    >
                      <TruncatedText
                        text={scope.name || ""}
                        maxLength={120}
                      />
                    </Typography>
                  </TableCell>
                  <TableCell align="center" className="scopes-actions-column">
                    <Stack direction="row" spacing={1} justifyContent="center">
                      <IconButton
                        className="scope-action-button scope-edit-button"
                        onClick={() => {
                          setEditableScope(scope);
                          setOpenEditPopup(true);
                        }}
                        size="small"
                      >
                        <EditIcon />
                      </IconButton>
                      <IconButton
                        className="scope-action-button scope-delete-button"
                        onClick={() => {
                          setScopeToDelete(scope);
                          setOpenDeletePopup(true);
                        }}
                        size="small"
                      >
                        <DeleteIcon />
                      </IconButton>
                    </Stack>
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
        <Box className="scopes-table-footer">
          {`${scopes.length} scopes`}
        </Box>
      </TableContainer>

      <AddScopePopup
        open={openAddPopup}
        onClose={() => setOpenAddPopup(false)}
        onEditing={(scope: Scope) => {
          setScopes([...scopes, scope]);
        }}
      />
      {editableScope && (
        <EditScopePopup
          open={openEditPopup}
          onClose={() => {
            setEditableScope(undefined);
            handleCloseEditPopup();
          }}
          onEditing={(updatedScope: Scope) => {
            setScopes(
              scopes.map((scope) =>
                scope.id === updatedScope.id ? updatedScope : scope
              )
            );
          }}
          editableScope={editableScope}
        />
      )}
      <DeletePopup
        entityTitle={scopeToDelete?.name ?? "Unknown"}
        open={openDeletePopup}
        onClose={() => {
          setOpenDeletePopup(false);
          setScopeToDelete(undefined);
        }}
        onConfirm={() => {
          if (scopeToDelete) {
            deleteScope(scopeToDelete);
          }
          setOpenDeletePopup(false);
          setScopeToDelete(undefined);
        }}
      />
    </Box>
  );
};
