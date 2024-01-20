import "./degrees-grid.scss";
import { Box } from "@mui/material";
import { DataGrid, GridActionsCellItem } from "@mui/x-data-grid";
import { GridColDef, GridRowId } from "@mui/x-data-grid/models";
import moment from "moment";
import { IDegree } from "../../types/global.typing";
import { GridActionsColDef } from "@mui/x-data-grid";

import DeleteIcon from "@mui/icons-material/Delete";
import FileCopyIcon from "@mui/icons-material/FileCopy";
import httpModule from "../../helpers/http.module";

function deleteCandidate(id: GridRowId) {
  console.log(id);
}

const column: GridColDef[] = [
  { field: "id", headerName: "ID", width: 100 },
  { field: "degreeName", headerName: "Degree Name", width: 200 },
  { field: "isAssociated", headerName: "Is Associated", width: 150 },
  {
    field: "creationTime",
    headerName: "Creation Time",
    width: 200,
    renderCell: (params) =>
      moment(params.row.creationTime).format("YYYY-MM-DD HH:mm:ss"),
  },
  {
    field: "actions",
    type: "actions",
    width: 80,
    getActions: (params) => [
      <GridActionsCellItem icon={<DeleteIcon />} label="Delete" />,
    ],
  },
];

interface IDegreeGridProps {
  data: IDegree[];
}

const DegreeGrid = ({ data }: IDegreeGridProps) => {
  return (
    <Box sx={{ width: "100%", height: 450 }} className="degrees-grid">
      <DataGrid
        rows={data}
        columns={column}
        getRowId={(row) => row.id}
        rowHeight={50}
      />
    </Box>
  );
};

export default DegreeGrid;
