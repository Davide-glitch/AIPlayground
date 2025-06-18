import React from 'react';
import { Box, CircularProgress, Typography, Stack } from '@mui/material';
import './EnhancedLoader.css';

interface EnhancedLoaderProps {
  message?: string;
  size?: number;
}

export const EnhancedLoader: React.FC<EnhancedLoaderProps> = ({ 
  message = 'Loading...', 
  size = 60 
}) => {
  return (
    <Box className="enhanced-loader-container">
      <Stack spacing={3} alignItems="center">
        <Box className="enhanced-loader-wrapper">
          <CircularProgress 
            size={size} 
            className="enhanced-loader-progress"
            thickness={3}
          />
          <Box className="enhanced-loader-glow" />
        </Box>
        
        <Typography 
          variant="h6" 
          className="enhanced-loader-text"
        >
          {message}
        </Typography>
        
        <Box className="enhanced-loader-dots">
          <Box className="dot dot-1" />
          <Box className="dot dot-2" />
          <Box className="dot dot-3" />
        </Box>
      </Stack>
    </Box>
  );
};
