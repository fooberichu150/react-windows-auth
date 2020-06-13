export const handleResponse = (response) => {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      const error = (data && data) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
};

export const requestBase = (() => {
  if (typeof window !== "undefined") {
    return {
      method: "POST",
      credentials: "include",
      mode: 'cors',
      headers: new Headers({
        Accept: "application/json",
        "Content-Type": "application/json",
      }),
    };
  } else
    return {
      method: "POST",
      credentials: "include",
      // mode: 'cors',
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    };
})();