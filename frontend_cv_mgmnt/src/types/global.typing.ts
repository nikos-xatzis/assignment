export interface IDegree {
  id: string;
  degreeName: string;
  isAssociated: boolean;
  creationTime: string;
}
export interface ICreateDegreeDto {
  degreeName: string;
}
export interface IUpdateDegreeDto {
  degreeName: string;
  isAssociated: boolean;
}
export interface ICandidate {
  id: string;
  firstName: string;
  lastName: string;
  emailAddress: string;
  mobile: string;
  resumeUrl: string;
  creationTime: string;
  degreeId: string;
  degreeName: string;
}
export interface ICreateCandidateDto {
  firstName: string;
  lastName: string;
  emailAddress: string;
  mobile: string;
  degreeId: string;
}
export interface IUpdateCandidateDto {
  firstName: string;
  lastName: string;
  emailAddress: string;
  mobile: string;
  resumeUrl: string;
  degreeId: string;
}
