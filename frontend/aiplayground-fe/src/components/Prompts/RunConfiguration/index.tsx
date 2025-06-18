import { FC, useEffect, useState } from "react";
import "./RunConfiguration.css";
import {
  Autocomplete,
  Box,
  Button,
  Checkbox,
  Chip,
  CircularProgress,
  Paper,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  TextField,
  Typography,
} from "@mui/material";
import { CheckBox, CheckBoxOutlineBlank, PlayArrow } from "@mui/icons-material";
import { Model } from "../../shared/types/Model";
import { ModelsApiClient } from "../../../api/Clients/ModelsApiClient";
import { ModelModel } from "../../../api/Models/ModelModel";
import { SliderComponent } from "../../common/SliderComponent";
import { Run } from "../../shared/types/Run";
import { RunsApiClient } from "../../../api/Clients/RunsApiClient";
import { RunCreateModel } from "../../../api/Models/RunCreateModel";
import { useParams } from "react-router-dom";
import { RunModel } from "../../../api/Models/RunModel";
import { TableHeader } from "../../common/TableHeader";
import { AIPlaygroundApiClient } from "../../../api/Base/BaseApiClient";

export const RunConfiguration: FC = () => {
  const [models, setModels] = useState<Model[]>([]);
  const [modelsLoading, setModelsLoading] = useState(false);
  const [selectedModels, setSelectedModels] = useState<Model[]>([]);
  const [temperature, setTemperature] = useState(0);
  const [runResults, setRunResults] = useState<Run[]>([]);
  const [running, setRunning] = useState(false);
  const { id } = useParams();

  const columns = [
    {
      id: "modelName",
      label: "Model",
    },
    {
      id: "temperature",
      label: "Temperature",
    },
    {
      id: "result",
      label: "Result",
    },
  ];

  const handleTemperatureChange = (_event: Event, value: number | number[]) => {
    const temperature = Array.isArray(value) ? value[0] : value;
    setTemperature(temperature);
  };

  const fetchModels = async () => {
    try {
      setModelsLoading(true);
      const res = await ModelsApiClient.getAllAsync();
      const fetchedModels = res.map((e: ModelModel) => ({ ...e } as Model));
      setModels(fetchedModels);
      setModelsLoading(false);
    } catch (error: any) {
      console.log(error);
      setModelsLoading(false);
    }
  };  const handleRun = async () => {
    try {
      setRunning(true);
      const runCreateModel: RunCreateModel = {
        modelsToRun: selectedModels.map((model) => {
          return {
            modelId: model.id,
            temperature: temperature,
          };
        }),
        promptId: parseInt(id ?? "0", 10),
      };
      
      console.log("=== RUN REQUEST DEBUG ===");
      console.log("Selected Models:", selectedModels);
      console.log("Temperature:", temperature);
      console.log("Prompt ID:", id);
      console.log("Parsed Prompt ID:", parseInt(id ?? "0", 10));
      console.log("Full Request Payload:", JSON.stringify(runCreateModel, null, 2));
      console.log("API URL:", `${AIPlaygroundApiClient.defaults.baseURL}${RunsApiClient.urlPath}`);
      console.log("========================");
      
      const result = await RunsApiClient.runAsync(runCreateModel);
      console.log("Received run response:", result);
      
      const runs = result.map((e: RunModel) => ({ ...e } as Run));
      setRunResults(runs);
      setRunning(false);
    } catch (error: any) {
      console.error("Error running models:", error);
      
      // More detailed error logging
      if (error.response) {
        console.error("=== BACKEND ERROR DETAILS ===");
        console.error("Status:", error.response.status);
        console.error("Status Text:", error.response.statusText);
        console.error("Response Data:", error.response.data);
        console.error("Response Headers:", error.response.headers);
        console.error("============================");
        
        let errorMessage = `Backend error: ${error.response.status} - ${error.response.statusText}`;
        if (error.response.data) {
          errorMessage += `\n\nDetails: ${JSON.stringify(error.response.data, null, 2)}`;
        }
        alert(errorMessage);
      } else if (error.request) {
        console.error("No response from backend:", error.request);
        alert("No response from backend server. Check if your .NET API is running.");
      } else {
        console.error("Request setup error:", error.message);
        alert(`Request error: ${error.message}`);
      }
      
      setRunning(false);
    }
  };

  const renderModelsSelect = () => {
    return (
      <Box>
        <Typography variant="h6">Select Models</Typography>
        <Autocomplete
          multiple
          disableCloseOnSelect
          options={models}
          value={selectedModels}
          onChange={(event, newValue) => setSelectedModels(newValue)}
          getOptionLabel={(option: Model) => option.name!}
          renderOption={(props, option, { selected }) => (
            <li {...props}>
              <Checkbox
                icon={<CheckBoxOutlineBlank fontSize="small" />}
                checkedIcon={<CheckBox fontSize="small" />}
                checked={selected}
              />
              {option.name}
            </li>
          )}
          renderInput={(params) => <TextField {...params} />}
          renderValue={(selectedModels: readonly Model[], getTagProps) =>
            selectedModels.map((option: Model, index: number) => (
              <Chip
                variant="outlined"
                label={option.name}
                {...getTagProps({ index })}
              />
            ))
          }
          sx={{ width: "100%" }}
        />
      </Box>
    );
  };

  useEffect(() => {
    fetchModels();
  }, []);

  if (modelsLoading) {
    return (
      <Stack justifyContent="center" alignItems="center" height="80vh">
        <CircularProgress size={60} />
      </Stack>
    );
  }

  return (
    <Stack>
      <Box>
        <Stack
          direction={"row"}
          spacing={2}
          alignItems={"center"}
          justifyContent={"space-between"}
        >
          <Typography variant="h5">Run configuration</Typography>
          <Button
            className="large-button"
            color="primary"
            variant="contained"
            onClick={handleRun}
            disabled={running || selectedModels.length === 0}
          >
            <PlayArrow className="large-icon" />
          </Button>
        </Stack>
      </Box>
      {renderModelsSelect()}
      <Typography style={{ color: 'black' }}>Temperature</Typography>
      <SliderComponent
        minValue={0}
        maxValue={1}
        step={0.1}
        value={temperature}
        onChange={handleTemperatureChange}
        name="temperature"
        color="black"
      />
      {running && (
        <>
          <Typography variant="h6" mt={2}>
            Running models, please wait...
          </Typography>
          <CircularProgress />
        </>
      )}
      {!running && runResults.length > 0 && (
        <Box mt={2}>
          <Typography variant="h6">Run results</Typography>
          <TableContainer component={Paper}>
            <Table>
              <TableHeader columns={columns} />
              <TableBody>
                {runResults.map((run) => {
                  const modelName =
                    models.find((m) => m.id === run.modelId)?.name ?? "Unknown";
                  return (
                    <TableRow key={run.id}>
                      <TableCell>{modelName}</TableCell>
                      <TableCell>{run.temperature}</TableCell>
                      <TableCell>{run.actualResponse}</TableCell>
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </TableContainer>
        </Box>
      )}
    </Stack>
  );
};
