function getErrorsForSolution() {
    return ajaxRequest({
        url: '/SolutionLibrary/GetErrorsForSolution',
        method: 'GET',
    })
}

function getSolutions() {
    return ajaxRequest({
        url: '/SolutionLibrary/GetSolutions',
        method: 'GET',
    })
}

function getSolutionById(id) {
    return ajaxRequest({
        url: '/SolutionLibrary/GetSolutionById',
        method: 'GET',
        data: { id: id }
    })
}

function addSolution(solutionDto) {
    const formData = new FormData();

    formData.append("errorId", solutionDto.errorId);
    formData.append("cause", solutionDto.cause);
    formData.append("handle", solutionDto.handle);
    formData.append("beforeFile", $("#addBeforeImage")[0].files[0]);
    formData.append("afterFile", $("#addAfterImage")[0].files[0]);

    return ajaxRequest({
        url: '/SolutionLibrary/AddSolution',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true
    })
}

function updateSolution(solutionDto) {
    const formData = new FormData();
    formData.append("id", solutionDto.id);
    formData.append("errorId", solutionDto.errorId);
    formData.append("cause", solutionDto.cause);
    formData.append("handle", solutionDto.handle);
    formData.append("beforeUrl", solutionDto.beforeUrl);
    formData.append("afterUrl", solutionDto.afterUrl);
    formData.append("beforeFile", $("#editBeforeImage")[0].files[0]);
    formData.append("afterFile", $("#editAfterImage")[0].files[0]);

    return ajaxRequest({
        url: '/SolutionLibrary/UpdateSolution',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true
    })
}

function deleteSolution(id) {
    return ajaxRequest({
        url: '/SolutionLibrary/DeleteSolution',
        method: 'POST',
        data: id,
        showLoading: true
    })
}