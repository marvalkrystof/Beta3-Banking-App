$(document).ready(function () {
    var employeesSelect = document.getElementById("employeeDropDown");
    var customersSelect = document.getElementById("customerDropDown");
    employeesSelect.addEventListener(
        'change',
        function () {
            customersSelect.value = '-1'
        },
        false
    );

    customersSelect.addEventListener(
        'change',
        function () {
            employeesSelect.value = '-1'
        },
        false
    );


}
);
