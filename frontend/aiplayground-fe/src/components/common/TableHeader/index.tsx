import { TableCell, TableHead, TableRow } from "@mui/material";
import { FC } from "react";

import "./TableHeader.css";

interface TableHeaderProps {
  columns: {
    id: string;
    label: string;
    className?: string; // Optional column-specific class
  }[];
}

export const TableHeader: FC<TableHeaderProps> = ({
  columns,
}: TableHeaderProps) => {
  return (
    <TableHead>
      <TableRow className={"table-header"}>
        {columns.map((column) => (
          <TableCell key={column.id} align="center" className={column.className || ''}>{column.label}</TableCell>
        ))}
      </TableRow>
    </TableHead>
  );
};
