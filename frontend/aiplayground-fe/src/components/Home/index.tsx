import { FC } from "react";
import { Box, Typography, Stack, Button, Card, CardContent } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { PlayArrow, AutoAwesome, TrendingUp, Psychology } from "@mui/icons-material";

import "./Home.css";

export const Home: FC = () => {
  const navigate = useNavigate();

  return (
    <>
      <Box className={"background-image"}></Box>
      <Box 
        sx={{ 
          padding: 4, 
          minHeight: '100vh',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center'
        }}
      >
        <Stack spacing={6} alignItems="center" maxWidth="1000px">
          {/* Hero Section */}
          <Stack spacing={3} alignItems="center" textAlign="center">
            <Typography 
              variant="h1" 
              sx={{ 
                fontSize: { xs: '2.5rem', md: '4rem' },
                fontWeight: 900,
                background: 'linear-gradient(135deg, #6366f1 0%, #8b5cf6 50%, #ec4899 100%)',
                WebkitBackgroundClip: 'text',
                WebkitTextFillColor: 'transparent',
                textShadow: '0 4px 20px rgba(99, 102, 241, 0.3)',
                animation: 'float 6s ease-in-out infinite'
              }}
            >
              AI Playground
            </Typography>
            
            <Typography 
              variant="h3" 
              sx={{ 
                fontSize: { xs: '1.25rem', md: '1.75rem' },
                color: '#475569',
                fontWeight: 600,
                maxWidth: '600px',
                opacity: 0.9
              }}
            >
              Explore, Compare & Optimize AI Models 
            </Typography>
          </Stack>

          {/* Feature Cards */}
          <Stack 
            direction={{ xs: 'column', md: 'row' }} 
            spacing={3} 
            sx={{ width: '100%' }}
          >
            <Card 
              className="hover-lift"
              sx={{ 
                flex: 1,
                background: 'rgba(99, 102, 241, 0.05)',
                border: '1px solid rgba(99, 102, 241, 0.2)',
                cursor: 'pointer'
              }}
              onClick={() => navigate('/platforms')}
            >
              <CardContent sx={{ textAlign: 'center', p: 3 }}>
                <AutoAwesome sx={{ fontSize: 48, color: '#6366f1', mb: 2 }} />
                <Typography variant="h6" gutterBottom sx={{ fontWeight: 700 }}>
                  AI Platforms
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Explore OpenAI, DeepSeek, Gemini and more with detailed model comparisons
                </Typography>
              </CardContent>
            </Card>

            <Card 
              className="hover-lift"
              sx={{ 
                flex: 1,
                background: 'rgba(139, 92, 246, 0.05)',
                border: '1px solid rgba(139, 92, 246, 0.2)',
                cursor: 'pointer'
              }}
              onClick={() => navigate('/prompts')}
            >
              <CardContent sx={{ textAlign: 'center', p: 3 }}>
                <Psychology sx={{ fontSize: 48, color: '#8b5cf6', mb: 2 }} />
                <Typography variant="h6" gutterBottom sx={{ fontWeight: 700 }}>
                  Smart Prompts
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Create, manage and test prompts across different scopes and contexts
                </Typography>
              </CardContent>
            </Card>

            <Card 
              className="hover-lift"
              sx={{ 
                flex: 1,
                background: 'rgba(236, 72, 153, 0.05)',
                border: '1px solid rgba(236, 72, 153, 0.2)',
                cursor: 'pointer'
              }}
              onClick={() => navigate('/runs')}
            >
              <CardContent sx={{ textAlign: 'center', p: 3 }}>
                <TrendingUp sx={{ fontSize: 48, color: '#ec4899', mb: 2 }} />
                <Typography variant="h6" gutterBottom sx={{ fontWeight: 700 }}>
                  Performance Analytics
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Analyze results, compare ratings and optimize your AI workflows
                </Typography>
              </CardContent>
            </Card>
          </Stack>

          {/* Call to Action */}
          <Stack spacing={3} alignItems="center">
            <Button
              variant="contained"
              size="large"
              startIcon={<PlayArrow />}
              onClick={() => navigate('/platforms')}
              sx={{
                fontSize: '1.1rem',
                px: 4,
                py: 1.5,
                borderRadius: 3,
                background: 'linear-gradient(135deg, #6366f1 0%, #8b5cf6 100%)',
                '&:hover': {
                  background: 'linear-gradient(135deg, #8b5cf6 0%, #ec4899 100%)',
                  transform: 'translateY(-3px)',
                  boxShadow: '0 12px 40px rgba(99, 102, 241, 0.4)'
                }
              }}
            >
              Start Exploring AI
            </Button>
          </Stack>
        </Stack>
      </Box>
    </>
  );
};
