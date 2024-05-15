import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import "./App.css";
import UploadOrders from "./components/UploadOrders";
import SalesData from "./components/SalesData";

function App() {
  return (
    <Router>
      <div className="container">
        <nav className="navbar navbar-expand-lg bg-body-tertiary">
          <div className="container-fluid">
            <Link className="navbar-brand" to="/">
              Sales
            </Link>
            <button
              className="navbar-toggler"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarSupportedContent"
              aria-controls="navbarSupportedContent"
              aria-expanded="false"
              aria-label="Toggle navigation"
            >
              <span className="navbar-toggler-icon"></span>
            </button>
            <div
              className="collapse navbar-collapse"
              id="navbarSupportedContent"
            >
              <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                <li className="nav-item">
                  <Link
                    className="nav-link active"
                    aria-current="page"
                    to="/dashboard"
                  >
                    Dashboard
                  </Link>
                </li>
                <li className="nav-item">
                  <Link className="nav-link" to="/import">
                    Import
                  </Link>
                </li>
              </ul>
            </div>
          </div>
        </nav>
        <Routes>
          <Route path="/dashboard" element={<SalesData />} />
          <Route path="/import" element={<UploadOrders />} />
          <Route path="/" element={<SalesData />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
