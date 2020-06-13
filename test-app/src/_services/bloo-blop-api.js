import { handleResponse, requestBase } from "../_helpers";

const apiBase = "https://localhost:44387/blooblop";

class BlooBlopService {
  get() {
    let request = Object.assign({}, requestBase, { method: "GET" });
    let url = `${apiBase}`;

    return fetch(url, request).then(handleResponse);
  }

}

const instance = Object.freeze(new BlooBlopService());
export { instance as BlooBlopService };
