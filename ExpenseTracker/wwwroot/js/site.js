const btnExpense = document.getElementById('btn-expense');
const btnIncome = document.getElementById('btn-income');
const btnCloseIncomeForm = document.getElementById('btn-close-income-form');
const btnCloseExpenseForm = document.getElementById('btn-close-expense-form');
const btnIncomeRequest = document.getElementById('btn-income-request');
const btnExpenseRequest = document.getElementById('btn-expense-request');

const errorIncomeFromMessage = document.getElementById('error-income-from');
const errorIncomeValueMessage = document.getElementById('error-income-value');
const balance = document.getElementById('balance');

// Get income and expense form
const incomeForm = document.getElementById('income-form');
const expenseForm = document.getElementById('expense-form');

// Get income and expense div 
const incomeSubmitForm = document.getElementById('incomeSubmitForm');
const expenseSubmitForm = document.getElementById('expenseSubmitForm');

// Get income input value
const incomeFrom = document.getElementById('income-from');
const incomeValue = document.getElementById('income-value');

// Get expense input value
const expenseFrom = document.getElementById('expense-from');
const expenseValue = document.getElementById('expense-value');

const table = document.getElementById('table');
const dateForm = document.getElementById('by-date');

const historyList = document.getElementById('history');

incomeSubmitForm.style.display = "none";
expenseSubmitForm.style.display = "none";

function updateBalance(income, expense) {
    let balanceSum = income - expense;
    balance.innerHTML = balanceSum.toFixed(2);
    if (income != null) {
        document.getElementById('money-plus').innerHTML = "+$" + income.toFixed(2);
    }
    else {
        document.getElementById('money-plus').innerHTML = "+$0.00";
    }
    if (expense != null) {
        document.getElementById('money-minus').innerHTML = "-$" + expense.toFixed(2);
    }
    else {
        document.getElementById('money-minus').innerHTML = "-$0.00";
    }
}

async function getAllIncomes() {
    const response = await fetch('Income/GetAllIncomes');
    const responeData = await response.json();
    var incomeSum = 0;
    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.incomeFrom}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
                 <td><span><i class="fa fa-times"></i></span></td>
             </tr>`

        table.innerHTML += div;

        incomeSum += item.value;
    });

    updateBalance(incomeSum, null);
}

async function getDailyIncomes() {
    const response = await fetch('Income/GetDailyIncomes');
    const responeData = await response.json();
    var incomeSum = 0;
    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            ` <tr class="table-success" >
                <td scope="row">Income</td>
                <td>${item.incomeFrom}</td>
                <td id="inc-table-value">${item.value}</td>
                <td>${date.toLocaleDateString()}</td>
             </tr >`

        table.innerHTML += div;

        incomeSum += item.value;
    });

    updateBalance(incomeSum, null);
}

async function getAllExpenses() {
    const response = await fetch('Expense/GetAllExpenses');
    const responeData = await response.json();
    var expenseSum = 0;
    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.expenseFrom}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

        table.innerHTML += div;

        expenseSum += item.value;
    });

    updateBalance(null, expenseSum);
}

async function getDailyExpenses() {
    const response = await fetch('Expense/GetDailyExpenses');
    const responeData = await response.json();
    var expenseSum = 0;

    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.expenseFrom}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`
        table.innerHTML += div;

        expenseSum += item.value;
    });

    updateBalance(null, expenseSum);
}

async function dailyHistory() {
    const responseDailyExpenses = await fetch('Expense/GetDailyExpenses');
    const responseDataDailyExpenses = await responseDailyExpenses.json();
    const responseDailyIncomes = await fetch('Income/GetDailyIncomes');
    const responseDataDailyIncomes = await responseDailyIncomes.json();
    var expenseSum = 0;
    var incomeSum = 0;
    // Concatenate expense and income in one object and sort them by datetime in descending order
    var objDailyHistory = responseDataDailyExpenses.concat(responseDataDailyIncomes);

    objDailyHistory.sort((a, b) => (a.dateTime < b.dateTime ? 1 : -1));

    table.innerHTML = '';

    objDailyHistory.forEach(function (item) {

        var date = new Date(item.dateTime);

        if (item.expenseFrom) {
            var div =
                `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.expenseFrom}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            expenseSum += item.value;
        } else {
            var div =
                `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.incomeFrom}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`
            incomeSum += item.value
        }

        table.innerHTML += div;
    });

    updateBalance(incomeSum, expenseSum);
}
async function getAllHistory() {
    const responseExpenses = await fetch('Expense/GetAllExpenses');
    const responseDataExpenses = await responseExpenses.json();
    const responseIncomes = await fetch('Income/GetAllIncomes');
    const responseDataIncomes = await responseIncomes.json();
    var expenseSum = 0;
    var incomeSum = 0;
    // Concatenate expense and income in one object and sort them by datetime in descending order
    var objHistory = responseDataExpenses.concat(responseDataIncomes);
    objHistory.sort((a, b) => (a.dateTime < b.dateTime ? 1 : -1));

    table.innerHTML = '';

    objHistory.forEach(function (item) {

        var date = new Date(item.dateTime);

        if (item.expenseFrom) {
            var div =
                `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.expenseFrom}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            expenseSum += item.value;
        } else {
            var div =
                `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.incomeFrom}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`
            incomeSum += item.value
        }

        table.innerHTML += div;
    });

    updateBalance(incomeSum, expenseSum);
}
async function getByDate() {
    const dateFromValue = document.getElementById('date-from').value;
    const dateToValue = document.getElementById('date-to').value;

    const responseExpenses = await fetch('Expense/GetAllExpenses');
    const responseDataExpenses = await responseExpenses.json();
    const responseIncomes = await fetch('Income/GetAllIncomes');
    const responseDataIncomes = await responseIncomes.json();

    var expenseSum = 0;
    var incomeSum = 0;
    // Concatenate expense and income in one object then filter by choosen date and sort them by datetime in descending order
    var objHistory = responseDataExpenses.concat(responseDataIncomes);
    var filterHistory = objHistory.filter(date => date.dateTime >= dateFromValue && date.dateTime <= dateToValue);
    var sortedHistory = filterHistory.sort((a, b) => (a.dateTime < b.dateTime ? 1 : -1));

    table.innerHTML = '';

    sortedHistory.forEach(function (item) {

        var date = new Date(item.dateTime);

        if (item.expenseFrom) {
            var div =
                `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.expenseFrom}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            expenseSum += item.value;
        } else {
            var div =
                `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.incomeFrom}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            incomeSum += item.value
        }

        table.innerHTML += div;
    });

    updateBalance(incomeSum, expenseSum);

}

function showByDateForm() {
    dateForm.style.display = "block";
    dateForm.innerHTML = '';
    var div = `<form method="get">
            <label for="from">From</label>
            <input id="date-from" type="date" />
            <label for="to">To</label>
            <input id="date-to" type="date" />
            <button onclick="getByDate()" type="button">Show</button>
            <span onclick="closeDateForm()" class="close-btn"><i class="fa fa-times"></i></span>
        </form>`
    dateForm.innerHTML += div;
}

function closeDateForm() {
    dateForm.style.display = "none";
}

function showHistory() {
    const allHistory = "all history";
    const dailyHistoy = "daily history";
    const byDate = "by date";
    const dailyIncomes = "daily incomes";
    const allIncomes = "all incomes";
    const dailyExpenses = "daily expenses";
    const allExpenses = "all expenses";

    const selected = historyList.options[historyList.selectedIndex].value;

    if (selected !== byDate) {
        dateForm.style.display = "none";
    }

    if (selected === allHistory) {
        getAllHistory();
    } else if (selected === dailyHistoy) {
        dailyHistory();
    } else if (selected === byDate) {
        showByDateForm();
    } else if (selected === dailyIncomes) {
        getDailyIncomes();
    } else if (selected === allIncomes) {
        getAllIncomes();
    } else if (selected === dailyExpenses) {
        getDailyExpenses();
    } else if (selected === allExpenses) {
        getAllExpenses();
    }
}

function closeIncomeForm() {
    incomeSubmitForm.style.display = "none";
    incomeForm.reset();
}

function closeExpenseForm() {
    expenseSubmitForm.style.display = "none";
    expenseForm.reset();
}

function showIncomeSubmitForm() {
    check = "income";

    if (expenseSubmitForm.style.display === "block") {
        expenseSubmitForm.style.display = "none"
    }

    if (incomeSubmitForm.style.display === "none") {
        incomeSubmitForm.style.display = "block";
    } else {
        incomeSubmitForm.style.display = "none";
    }
}

function showExpenseSubmitForm() {
    check = "expense";
    if (incomeSubmitForm.style.display === 'block') {
        incomeSubmitForm.style.display = "none"
    }

    if (expenseSubmitForm.style.display === "none") {
        expenseSubmitForm.style.display = "block";
    } else {
        expenseSubmitForm.style.display = "none";
    }
}

function checkIncomeRequiredField(string, value) {
    errorIncomeFromMessage.innerHTML = '';
    errorIncomeValueMessage.innerHTML = '';
    const pattern = /^[0-9]+([.][0-9]+)?$/;
    const match = value.match(pattern);

    if (string === "") {
        errorIncomeFromMessage.style.color = '#e74c3c';
        errorIncomeFromMessage.innerText = `This field is required.`;   
    }
    if (value === "") {
        errorIncomeValueMessage.style.color = '#e74c3c';
        errorIncomeValueMessage.innerText = 'This field is required.';
    } else if (match === null) {
        errorIncomeValueMessage.style.color = '#e74c3c';
        errorIncomeValueMessage.innerText = 'This price must be only numbers.';
    }

    if (string !== "" && match !== null) {
        errorIncomeFromMessage.innerHTML = '';
        errorIncomeValueMessage.innerHTML = '';
        return true;
    }
}

// post create request
async function sendIncomeRequest() {

    let url = '/Income/CreateIncome';
    let xhr = new XMLHttpRequest();

    if (checkIncomeRequiredField(incomeFrom.value, incomeValue.value) === true) {
        xhr.open("POST", url, true)
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                dailyHistory();
            }
        };
        xhr.open("POST", url, true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        let body = await JSON.stringify({ incomeFrom: incomeFrom.value, value: incomeValue.value });
        xhr.send(body);
        incomeForm.reset();
    }     
}

async function sendExpenseRequest() {

    let url = '/Expense/CreateExpense';
    let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            dailyHistory();
        }
    };
    xhr.open("POST", url, true);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    let body = await JSON.stringify({ expenseFrom: expenseFrom.value, value: expenseValue.value });
    xhr.send(body);

    expenseForm.reset();
}

$(table).ready(function () {
    dailyHistory();
});

$(historyList).change(function () {
    showHistory();
});

$(btnCloseIncomeForm).click(function () {
    closeIncomeForm();
});

$(btnCloseExpenseForm).click(function () {
    closeExpenseForm();
});

$(btnIncome).click(function () {
    showIncomeSubmitForm();
});

$(btnExpense).click(function () {
    showExpenseSubmitForm();
});

$(btnIncomeRequest).click(function () {
    sendIncomeRequest();
});

$(btnExpenseRequest).click(function () {
    sendExpenseRequest();
});







