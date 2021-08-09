var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

function updateFields(val, id) {
    var serviceFee = 0;
    var actualRate = 0;
    var hourlyRate = 0;

    if (id === 'actualRateInput') {
        hourlyRate = val / 0.8;
        actualRate = Number(val);
    }
    else {
        actualRate = val * 0.8;
        hourlyRate = Number(val);
    }

    serviceFee = actualRate - hourlyRate;

    document.getElementById('serviceFeeLabel').innerText = serviceFee.toFixed(2);
    document.getElementById('actualRateInput').value = actualRate.toFixed(2);
    document.getElementById('hourlyRateInput').value = hourlyRate.toFixed(2);
}