import { useEffect, useState } from "react";
import "./degrees.scss";
import httpModule from "../../helpers/http.module";
import { IDegree } from "../../types/global.typing";
import { Button, CircularProgress } from "@mui/material";
import { Add } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import DegreeGrid from "../../components/degrees/DegreeGrid.component";

const Degrees = () => {
  const [degrees, setDegrees] = useState<IDegree[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const redirect = useNavigate();

  useEffect(() => {
    setLoading(true);
    httpModule
      .get<IDegree[]>("/Degree/Get")
      .then((response) => {
        setDegrees(response.data);
        setLoading(false);
      })
      .catch((error) => {
        alert("Error");
        console.log(error);
        setLoading(false);
      });
  }, []);

  return (
    <div className="content degrees">
      <div className="heading">
        <h2>Degrees</h2>
        <Button variant="outlined" onClick={() => redirect("/degrees/add")}>
          <Add />
        </Button>
      </div>
      {loading ? (
        <CircularProgress size={100} />
      ) : degrees.length === 0 ? (
        <h1>No Degrees</h1>
      ) : (
        <DegreeGrid data={degrees} />
      )}
    </div>
  );
};

export default Degrees;
