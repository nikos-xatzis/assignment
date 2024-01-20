import { useState, useEffect } from "react";
import "./candidates.scss";
import { IDegree, ICreateCandidateDto } from "../../types/global.typing";

import TextField from "@mui/material/TextField/TextField";
import FormControl from "@mui/material/FormControl/FormControl";
import InputLabel from "@mui/material/InputLabel/InputLabel";
import Select from "@mui/material/Select/Select";
import MenuItem from "@mui/material/MenuItem/MenuItem";
import Button from "@mui/material/Button/Button";
import { useNavigate } from "react-router-dom";
import httpModule from "../../helpers/http.module";

const AddCandidate = () => {
  const [candidate, setCandidate] = useState<ICreateCandidateDto>({
    firstName: "",
    lastName: "",
    emailAddress: "",
    mobile: "",
    degreeId: "",
  });

  const [degrees, setDegrees] = useState<IDegree[]>([]);
  const [pdfFile, setPdfFile] = useState<File | null>();

  const redirect = useNavigate();

  useEffect(() => {
    httpModule
      .get<IDegree[]>("/Degree/Get")
      .then((response) => {
        setDegrees(response.data);
      })
      .catch((error) => {
        alert("Error");
        console.log(error);
      });
  }, []);

  const handleClickSaveBtn = () => {
    if (
      candidate.firstName === "" ||
      candidate.lastName === "" ||
      candidate.emailAddress === "" ||
      candidate.mobile === "" ||
      candidate.degreeId === "" ||
      !pdfFile
    ) {
      alert("Fill all fields");
      return;
    }
    const newCandidateFormData = new FormData();
    newCandidateFormData.append("firstName", candidate.firstName);
    newCandidateFormData.append("lastName", candidate.lastName);
    newCandidateFormData.append("emailAddress", candidate.emailAddress);
    newCandidateFormData.append("mobile", candidate.mobile);
    newCandidateFormData.append("degreeId", candidate.degreeId);
    newCandidateFormData.append("pdfFile", pdfFile);
    httpModule
      .post("/Candidate/CreateCandidate", newCandidateFormData)
      .then((response) => redirect("/candidates"))
      .catch((error) => console.log(error));
  };

  const handleClickBackBtn = () => {
    redirect("/candidates");
  };

  return (
    <div className="content">
      <div className="add-candidate">
        <h2>Add New Candidate</h2>
        <FormControl fullWidth>
          <InputLabel>Degree</InputLabel>
          <Select
            value={candidate.degreeId}
            label="Degree"
            onChange={(e) =>
              setCandidate({ ...candidate, degreeId: e.target.value })
            }
          >
            {degrees.map((item) => (
              <MenuItem key={item.id} value={item.id}>
                {item.degreeName}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <TextField
          autoComplete="off"
          label="First Name"
          variant="outlined"
          value={candidate.firstName}
          onChange={(e) =>
            setCandidate({ ...candidate, firstName: e.target.value })
          }
        />
        <TextField
          autoComplete="off"
          label="Last Name"
          variant="outlined"
          value={candidate.lastName}
          onChange={(e) =>
            setCandidate({ ...candidate, lastName: e.target.value })
          }
        />
        <TextField
          autoComplete="off"
          label="Email"
          variant="outlined"
          value={candidate.emailAddress}
          onChange={(e) =>
            setCandidate({ ...candidate, emailAddress: e.target.value })
          }
        />
        <TextField
          autoComplete="off"
          label="Mobile"
          variant="outlined"
          value={candidate.mobile}
          onChange={(e) =>
            setCandidate({ ...candidate, mobile: e.target.value })
          }
        />
        <input
          type="file"
          onChange={(event) =>
            setPdfFile(event.target.files ? event.target.files[0] : null)
          }
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

export default AddCandidate;
