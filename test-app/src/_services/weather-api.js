import { handleResponse, requestBase } from "../_helpers";

const apiBase = "https://localhost:44387/weatherforecast";

class WeatherForecastService {

  getForecasts() {
    let request = Object.assign({}, requestBase, { method: "GET" });
    let url = `${apiBase}`;

    return fetch(url, request).then(handleResponse);
  }

  getProtectedForecast() {
    let request = Object.assign({}, requestBase, { method: "GET" });
    let url = `${apiBase}/5`;

    return fetch(url, request).then(handleResponse);
  }
}

const instance = Object.freeze(new WeatherForecastService());
export { instance as WeatherForecastService };
