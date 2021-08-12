function CheckError(response) {
    if (response.ok) {
        return response.json();
    } else {
        throw Error(response.statusText);
    }
}  