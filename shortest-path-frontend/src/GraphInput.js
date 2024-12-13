import React, { useState } from "react";
import axios from "axios";
import "./GraphInput.css";
import no_result from "../src/assets/005.png";
import { FaCalculator } from "react-icons/fa";

const GraphInput = () => {
  const [fromNode, setFromNode] = useState("A");
  const [toNode, setToNode] = useState("");
  const [result, setResult] = useState(null);

  const nodes = ["A", "B", "C", "D", "E", "F", "G"];

  const handleSubmit = async () => {
    const response = await axios.post(
      "https://localhost:7208/api/ShortestPath/calculate",
      {
        fromNode,
        toNode,
      }
    );

    setResult({
      path: response.data.pathStr,
      distance: response.data.result.distance,
    });
  };

  const handleClear = () => {
    setFromNode("A");
    setToNode("");
    setResult(null);
  };

  return (
    <div className="graph-input-container">
      <div className="form-section">
        <h2 className="header-label">Select Path</h2>
        <div className="form-group">
          <label className="form-label">From Node</label>
          <select
            value={fromNode}
            onChange={(e) => setFromNode(e.target.value)}
            className="dropdown"
          >
            {nodes.map((node) => (
              <option key={node} value={node}>
                {node}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group">
          <label className="form-label">To Node</label>
          <select
            value={toNode}
            onChange={(e) => setToNode(e.target.value)}
            className="dropdown"
          >
            {nodes.map((node) => (
              <option key={node} value={node}>
                {node}
              </option>
            ))}
          </select>
        </div>

        <div className="button-group">
          <button onClick={handleClear} className="clear-button">
            Clear
          </button>
          <button onClick={handleSubmit} className="calculate-button">
            Calculate &nbsp;
            <FaCalculator />
          </button>
        </div>
      </div>
      {result ? (
        <div className="result-container">
          <h2 className="header-label">Result</h2>
          <div>
            <div className="result-section">
              <p>{result.path}</p>
              <br />
              <p>Total Distance: {result.distance}</p>
            </div>
          </div>
        </div>
      ) : (
        <div className="no-result-img">
          <img src={no_result} alt="" />
        </div>
      )}
    </div>
  );
};

export default GraphInput;
