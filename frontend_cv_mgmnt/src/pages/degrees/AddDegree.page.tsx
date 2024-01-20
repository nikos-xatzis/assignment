import { useState, useEffect } from "react";
import "./degrees.scss";
import { ICreateDegreeDto } from "../../types/global.typing";

import TextField from "@mui/material/TextField/TextField";
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";

const AddDegree = () => {
  const [degree, setDegree] = useState<ICreateDegreeDto>({ degreeName: "" });
  const redirect = useNavigate();

  const handleClickSaveBtn = () => {
    if (degree.degreeName === "") {
      alert("Fill all fields");
      return;
    }
    httpModule
      .post("/Degree/CreateDegree", degree)
      .then((response) => redirect("/degrees"))
      .catch((error) => console.log(error));
  };

  const handleClickBackBtn = () => {
    redirect("/degrees");
  };

  return (
    <div className="content">
      <div className="add-degree">
        <h2>Add New Degree</h2>
        <TextField
          autoComplete="off"
          label="Degree Name"
          variant="outlined"
          value={degree.degreeName}
          onChange={(e) => setDegree({ ...degree, degreeName: e.target.value })}
        />

        <div className="btns">
          <Button
            variant="outlined"
            color="primary"
            onClick={handleClickSaveBtn}
          >
            Save
          </Button>
          <Button
            variant="outlined"
            color="secondary"
            onClick={handleClickBackBtn}
          >
            Back
          </Button>
        </div>
      </div>
    </div>
  );
};

export default AddDegree;
