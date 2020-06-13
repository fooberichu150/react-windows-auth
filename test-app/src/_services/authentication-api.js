import { handleResponse, requestBase } from "../_helpers";

const apiBase = "https://localhost:44387/authentication";

class AuthenticationService {
  getRoles() {
    let request = Object.assign({}, requestBase, { method: "GET" });
    let url = `${apiBase}`;

    return fetch(url, request).then(handleResponse);
  }
}

const instance = Object.freeze(new AuthenticationService());
export { instance as AuthenticationService };
