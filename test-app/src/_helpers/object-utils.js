export const isNullOrUndefined = (obj) => {
    return obj === undefined || obj === null;
}

export const isNullOrEmpty = (objArray) => {
    if (isNullOrUndefined(objArray))
        return true;

    if (!Array.isArray(objArray))
        throw Error('Object must be an array');

    return objArray.length === 0;
}