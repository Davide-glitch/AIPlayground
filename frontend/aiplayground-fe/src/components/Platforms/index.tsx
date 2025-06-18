import { FC, useEffect, useState } from "react";

import { Platform } from "../shared/types/Platform";

import "./Platforms.css";
import { PlatformsApiClient } from "../../api/Clients/PlatformsApiClient";
import { PlatformModel } from "../../api/Models/PlatformModel";
import { ModelModel } from "../../api/Models/ModelModel";
import { Model } from "../shared/types/Model";
import {
  Box,
  Card,
  CardContent,
  CardMedia,
  CircularProgress,
  Grid,
  List,
  ListItem,
  ListItemText,
  Paper,
  Stack,
  Typography,
  Chip,
  Divider,
} from "@mui/material";
import { RatingDisplay } from "../common/RatingDisplay";
import { AutoAwesome, TrendingUp } from "@mui/icons-material";

export const Platforms: FC = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [platforms, setPlatforms] = useState<Platform[]>([]);

  const fetchPlatforms = async () => {
    try {
      setIsLoading(true);

      const res = await PlatformsApiClient.getAllAsync();

      const fetchedPlatforms = res.map(
        (e: PlatformModel): Platform => ({
          id: e.id!,
          name: e.name,
          imageUrl: e.imageUrl,
          models: e.models.map(
            (model: ModelModel): Model => ({
              id: model.id!,
              name: model.name,
              averageRating: model.averageRating,
              averageUserRating: model.averageUserRating,
            })
          ),
        })
      );

      setPlatforms(fetchedPlatforms);

      setIsLoading(false);
    } catch (error: any) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetchPlatforms();
  }, []);

  if (isLoading) {
    return (
      <Stack justifyContent="center" alignItems="center" height="80vh">
        <CircularProgress
          size={60}
          sx={{
            color: "#6366f1",
            "& .MuiCircularProgress-circle": {
              strokeLinecap: "round",
            },
          }}
        />
        <Typography
          variant="h6"
          sx={{ mt: 2, color: "#64748b", fontWeight: 600 }}
        >
          Loading AI Platforms...
        </Typography>
      </Stack>
    );
  }

  return (
    <Box sx={{ p: 4, minHeight: "100vh" }}>
      {/* Enhanced Header */}
      <Box className="platforms-header">
        <Stack
          direction="row"
          alignItems="center"
          justifyContent="center"
          spacing={2}
          mb={2}
        >
          <AutoAwesome sx={{ fontSize: 40, color: "#6366f1" }} />
          <Typography variant="h2" className="platforms-title">
            AI Platforms
          </Typography>
        </Stack>
        <Typography variant="body1" className="platforms-subtitle">
          Explore and compare different AI platforms with their models and
          performance metrics
        </Typography>
        <Stack direction="row" spacing={2} justifyContent="center" mt={3}>
          <Chip
            label={`${platforms.length} Platforms Available`}
            color="primary"
            icon={<TrendingUp />}
            sx={{ fontWeight: 600 }}
          />
          <Chip
            label={`${platforms.reduce(
              (sum, p) => sum + p.models.length,
              0
            )} Models Total`}
            color="secondary"
            sx={{ fontWeight: 600 }}
          />
        </Stack>
      </Box>

      <Grid container spacing={4}>
        {platforms.map((platform) => (
          <Grid size={{ xs: 12, sm: 6, md: 4 }} key={platform.id}>
            <Card className={"platforms-card hover-lift"}>
              <CardMedia
                component="img"
                height="350"
                src={platform.imageUrl}
                alt={platform.name}
                className={"platforms-card-media"}
              />
              <CardContent sx={{ p: 3 }}>
                <Stack spacing={2}>
                  <Typography
                    variant="h5"
                    component="h2"
                    sx={{
                      fontWeight: 700,
                      color: "#1e293b",
                      textAlign: "center",
                    }}
                  >
                    {platform.name}
                  </Typography>

                  <Divider
                    sx={{
                      background:
                        "linear-gradient(90deg, transparent, #6366f1, transparent)",
                    }}
                  />

                  <Typography
                    variant="subtitle1"
                    sx={{
                      fontWeight: 600,
                      color: "#475569",
                      textAlign: "center",
                    }}
                  >
                    Available Models ({platform.models.length})
                  </Typography>

                  <List sx={{ p: 0 }}>
                    {platform.models.map((model) => (
                      <ListItem
                        key={model.id}
                        className="model-card"
                        sx={{
                          borderRadius: 2,
                          mb: 1,
                          p: 2,
                        }}
                      >
                        <ListItemText
                          primary={
                            <Typography
                              variant="subtitle2"
                              sx={{ fontWeight: 600, color: "#1e293b" }}
                            >
                              {model.name}
                            </Typography>
                          }
                          secondary={
                            <Stack direction="row" spacing={1} mt={1}>
                              <RatingDisplay rating={model.averageRating} />
                              <RatingDisplay rating={model.averageUserRating} />
                            </Stack>
                          }
                        />
                      </ListItem>
                    ))}
                  </List>
                </Stack>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
};
