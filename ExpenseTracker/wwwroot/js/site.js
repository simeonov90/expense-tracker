const btnExpense = document.getElementById('btn-expense');
const btnIncome = document.getElementById('btn-income');
const btnCloseIncomeForm = document.getElementById('btn-close-income-form');
const btnCloseExpenseForm = document.getElementById('btn-close-expense-form');
const btnIncomeRequest = document.getElementById('btn-income-request');
const btnExpenseRequest = document.getElementById('btn-expense-request');
const paginationForm = document.getElementById('pagination');
const btnPrevious = document.getElementById('btn-previous');
const btnNext = document.getElementById('btn-next');

const errorIncomeFromMessage = document.getElementById('error-income-from');
const errorIncomeValueMessage = document.getElementById('error-income-value');
const errorExpenseFromMessage = document.getElementById('error-expense-from');
const errorExpenseValueMessage = document.getElementById('error-expense-value');
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
const tableTitle = document.getElementById('table-title');
// RegEx match only numbers and decimal numbers
const pattern = /^[0-9]+([.][0-9]+)?$/;
const itemsPerPage = 15;
var page = 1;
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
                 <td>${item.from}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
                 <td><span><i class="fa fa-times"></i></span></td>
             </tr>`

        table.innerHTML += div;

        incomeSum += item.value;
    });

    updateBalance(incomeSum, null);
    checkForPaging(responeData.length);
}

async function getDailyIncomes() {
    const response = await fetch('Income/GetDailyIncomes');
    const responeData = await response.json();
    var incomeSum = 0;
    table.innerHTML = '';
    responeData.forEach(function (item) {
        var date = new Date(item.dateTime);
        var div =
            ` <tr class="table-success">
                <td scope="row">Income</td>
                <td>${item.from}</td>
                <td id="inc-table-value">${item.value}</td>
                <td>${date.toLocaleDateString()}</td>
             </tr >`

        table.innerHTML += div;

        incomeSum += item.value;
    });

    updateBalance(incomeSum, null);
    checkForPaging(responeData.length);
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
                 <td>${item.from}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

        table.innerHTML += div;

        expenseSum += item.value;
    });

    updateBalance(null, expenseSum);
    checkForPaging(responeData.length);
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
                 <td>${item.from}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`
        table.innerHTML += div;

        expenseSum += item.value;
    });

    updateBalance(null, expenseSum);
    checkForPaging(responeData.length);
}

async function getDailyHistory() {
    const responseDailyHistory = await fetch('History/GetDaily');
    const responseDataDailyHistory = await responseDailyHistory.json();

    const incomeHistory = "Income";

    var expenseSum = 0;
    var incomeSum = 0;

    table.innerHTML = '';

    responseDataDailyHistory.forEach(function (item) {

        var date = new Date(item.dateTime);

        if (item.type === incomeHistory) {
            var div =
                `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.from}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            incomeSum += item.value
        } else {
            var div =
                `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.from}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            expenseSum += item.value;
        }

        table.innerHTML += div;
    });

    updateBalance(incomeSum, expenseSum);
    checkForPaging(responseDataDailyHistory.length);
}
async function getAllHistory() {
    const responseIncomeSum = await fetch('income/sumfromincomes');
    const responseDataIncomeSum = await responseIncomeSum.json();
    const responseExpenseSum = await fetch('expense/sumfromexpenses');
    const responseDataExpenseSum = await responseExpenseSum.json();
    const responseAllHistory = await fetch(`history/getall/${page}/${itemsPerPage}`);
    const responseDataAllHistory = await responseAllHistory.json();

    table.innerHTML = '';
    responseDataAllHistory.forEach(function (item) {
        var date = new Date(item.dateTime);

        if (item.type === "Income") {
            var div =
                `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.from}</td>
                 <td id="exp-table-value">${(item.value)}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`
        } else {
            var div =

                `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.from}</td>
                 <td id="inc-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`
        }
        table.innerHTML += div;
    });

    updateBalance(responseDataIncomeSum, responseDataExpenseSum);
    checkForPaging(responseDataAllHistory.length);
}
async function getByDate() {
    const dateFromValue = document.getElementById('date-from').value;
    const dateToValue = document.getElementById('date-to').value;
    const responseGetByDate = await fetch(`history/getbydate/${dateFromValue}/${dateToValue}/${page}/${itemsPerPage}`);
    const responseDataGetByDate = await responseGetByDate.json();

    var expenseSum = 0;
    var incomeSum = 0;
    table.innerHTML = '';

    responseDataGetByDate.forEach(function (item) {

        var date = new Date(item.dateTime);

        if (item.type === "Income") {
            var div =
                `<tr class="table-success">
                 <td scope="row">Income</td>
                 <td>${item.from}</td>
                 <td id="inc-table-value">${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            incomeSum += item.value;
        } else {
            var div =
                `<tr class="table-danger">
                 <td scope="row">Expense</td>
                 <td>${item.from}</td>
                 <td id="exp-table-value">-${item.value}</td>
                 <td>${date.toLocaleDateString()}</td>
             </tr>`

            expenseSum += item.value
        }

        table.innerHTML += div;
    });
    
    updateBalance(incomeSum, expenseSum);
    checkForPaging(responseDataGetByDate.length);

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
    const allHistory = "All history";
    const dailyHistory = "Daily history";
    const byDate = "By date";
    const dailyIncomes = "Daily incomes";
    const allIncomes = "All incomes";
    const dailyExpenses = "Daily expenses";
    const allExpenses = "All expenses";
    const selected = historyList.options[historyList.selectedIndex].value;

    if (selected !== byDate) {
        dateForm.style.display = "none";
    }

    if (selected === allHistory) {
        getAllHistory();
        tableTitle.innerText = allHistory;
    } else if (selected === dailyHistory) {
        getDailyHistory();
        tableTitle.innerText = dailyHistory;
    } else if (selected === byDate) {
        showByDateForm();
        tableTitle.innerText = byDate;
    } else if (selected === dailyIncomes) {
        getDailyIncomes();
        tableTitle.innerText = dailyIncomes;
    } else if (selected === allIncomes) {
        getAllIncomes();
        tableTitle.innerText = allIncomes;
    } else if (selected === dailyExpenses) {
        getDailyExpenses();
        tableTitle.innerText = dailyExpenses;
    } else if (selected === allExpenses) {
        getAllExpenses();
        tableTitle.innerText = allExpenses;
    }
}

function closeIncomeForm() {
    incomeSubmitForm.style.display = "none";
    errorIncomeFromMessage.innerHTML = '';
    errorIncomeValueMessage.innerHTML = '';
    incomeForm.reset();
}

function closeExpenseForm() {
    expenseSubmitForm.style.display = "none";
    errorExpenseFromMessage.innerHTML = '';
    errorExpenseValueMessage.innerHTML = '';
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

    let match = value.match(pattern);

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

function checkExpenseRequiredField(string, value) {
    errorExpenseFromMessage.innerHTML = '';
    errorExpenseValueMessage.innerHTML = '';

    let match = value.match(pattern);

    if (string === "") {
        errorExpenseFromMessage.style.color = '#e74c3c';
        errorExpenseFromMessage.innerText = `This field is required.`;
    }

    if (value === "") {
        errorExpenseValueMessage.style.color = '#e74c3c';
        errorExpenseValueMessage.innerText = 'This field is required.';
    } else if (match === null) {
        errorExpenseValueMessage.style.color = '#e74c3c';
        errorExpenseValueMessage.innerText = 'This price must be only numbers.';
    }

    if (string !== "" && match !== null) {
        errorExpenseFromMessage.innerHTML = '';
        errorExpenseValueMessage.innerHTML = '';
        return true;
    }
}

// post create request
async function sendIncomeRequest() {

    let url = '/Income/CreateIncome';
    let xhr = new XMLHttpRequest();

    if (checkIncomeRequiredField(incomeFrom.value, incomeValue.value)) {
        xhr.open("POST", url, true)
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8')
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                showHistory();
            }
        };
        xhr.open("POST", url, true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        let body = await JSON.stringify({ from: incomeFrom.value, value: incomeValue.value });
        xhr.send(body);
        incomeForm.reset();
    }
}

async function sendExpenseRequest() {

    let url = '/Expense/CreateExpense';
    let xhr = new XMLHttpRequest();

    if (checkExpenseRequiredField(expenseFrom.value, expenseValue.value)) {
        xhr.open("POST", url, true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                showHistory();
            }
        };
        let body = await JSON.stringify({ from: expenseFrom.value, value: expenseValue.value });
        xhr.send(body);
        expenseForm.reset();
    }

}
function checkForPaging(length) {
    if (page === 1 && length >= itemsPerPage) {
        paginationForm.style.display = 'block';  
    } else if (page <= 1 && length < itemsPerPage) {
        paginationForm.style.display = 'none';
    }

    if (page > 1) {
        btnPrevious.style.visibility = 'visible';
    } else {
        btnPrevious.style.visibility = 'hidden';
    }

    if (length < 14) {
        btnNext.style.visibility = 'hidden';
    } else {
        btnNext.style.visibility = 'visible';
    }
}

$(table).ready(function () {
    tableTitle.innerText = "Daily History";

    getDailyHistory();
});

$(historyList).change(function () {
    page = 1;
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

$(btnNext).click(function () {
    page++;
    if (historyList.value === "By date") {
        getByDate();
    } else {
        showHistory();
    }
});

$(btnPrevious).click(function () {
    page--;
    if (historyList.value === "By date") {
        getByDate();
    } else {
        showHistory();
    }
});




