import React from "react";
import logo from "./logo.svg";
import "./App.css";

import { AuthenticationService } from "./_services/authentication-api";
import { WeatherForecastService } from "./_services/weather-api";
import { BlooBlopService } from "./_services/bloo-blop-api";

function App() {
  const getForecasts = () => {
    console.log("attempting...");

    WeatherForecastService.getForecasts()
      .then((response) => {
        console.log("response: ", JSON.stringify(response));
        console.log("oh boy!");
      })
      .catch((err) => {
        console.error(err);
      });
  };

  const getProtectedForecast = () => {
    console.log("attempting...");

    WeatherForecastService.getProtectedForecast()
      .then((response) => {
        console.log("response: ", JSON.stringify(response));
        console.log("oh boy!");
      })
      .catch((err) => {
        console.error(err);
      });
  };

  const getRoles = () => {
    console.log("attempting to load roles...");

    AuthenticationService.getRoles()
      .then((response) => {
        console.log("got the roles, yo!");
        console.log("roles: ", JSON.stringify(response));
      })
      .catch((err) => {
        console.error(err);
      });
  };

  const getBlooBlop = () => {
    console.log("attempting to get bloo-blop...");

    BlooBlopService.get()
      .then((response) => {
        console.log("got your bloo-blop!");
        console.log(response);
      })
      .catch((err) => {
        console.error(err);
      });
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <div className="button-panel">
          <button type="button" onClick={getForecasts}>
            Get forecasts
          </button>
          <button type="button" onClick={getProtectedForecast}>
            Get protected forecast
          </button>
          <button type="button" onClick={getRoles}>
            Get roles
          </button>
          <button type="button" onClick={getBlooBlop}>
            Get Bloo-blop
          </button>
        </div>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
