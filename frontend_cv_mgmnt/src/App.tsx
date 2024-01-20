import { useContext, lazy, Suspense } from "react";
import Navbar from "./components/navbar/Navbar.component";
import { ThemeContext } from "./context/theme.context";
import { Routes, Route } from "react-router-dom";
import CustomLinearProgress from "./components/custom-linear-progress/CustomLinearProgress.component";
import Degrees from "./pages/degrees/Degrees.page";
import AddDegree from "./pages/degrees/AddDegree.page";
import Candidates from "./pages/candidates/Candidates.page";
import AddCandidate from "./pages/candidates/AddCandidate.page";

// Imports with Lazy loading
const Home = lazy(() => import("./pages/home/Home.page"));

const App = () => {
  const { darkMode } = useContext(ThemeContext);
  const appStyles = darkMode ? "app dark" : "app";

  return (
    <div className={appStyles}>
      <Navbar />
      <div className="wrapper">
        <Suspense fallback={<CustomLinearProgress />}>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/degrees">
              <Route index element={<Degrees />} />
              <Route path="add" element={<AddDegree />} />
            </Route>
            <Route path="/candidates">
              <Route index element={<Candidates />} />
              <Route path="add" element={<AddCandidate />} />
            </Route>
          </Routes>
        </Suspense>
      </div>
    </div>
  );
};

export default App;
