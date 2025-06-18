import React, { useState } from 'react';
import { Box, Chip, Tooltip, IconButton } from '@mui/material';
import { ExpandMore, ExpandLess } from '@mui/icons-material';

interface RatingDisplayProps {
  rating: number;
}

export const RatingDisplay: React.FC<RatingDisplayProps> = ({ rating }) => {
  const [showFull, setShowFull] = useState(false);
  
  // Handle NaN, null, undefined values - show nothing if invalid
  if (rating == null || isNaN(rating)) {
    return null;
  }
  
  const validRating = rating;
  const roundedRating = Math.round(validRating * 100) / 100; // Round to 2 decimal places
  const hasMoreDecimals = validRating !== roundedRating;
  
  const handleToggle = () => {
    setShowFull(!showFull);
  };
  
  return (
    <Box display="flex" alignItems="center" gap={1}>
      <Chip
        label={showFull ? validRating.toFixed(6) : roundedRating.toFixed(1)}
        color={validRating >= 4 ? 'success' : validRating >= 3 ? 'warning' : 'error'}
        size="small"
      />
      {hasMoreDecimals && (
        <Tooltip title={showFull ? 'Show less' : 'Show full rating'}>
          <IconButton size="small" onClick={handleToggle}>
            {showFull ? <ExpandLess /> : <ExpandMore />}
          </IconButton>
        </Tooltip>
      )}
    </Box>
  );
};
