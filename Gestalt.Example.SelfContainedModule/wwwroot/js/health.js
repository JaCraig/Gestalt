// Fetch the healthcheck endpoint and display the result in the currentHealth div
fetch("/healthcheck")
    .then(response => response.text())
    .then(data => {
        const currentHealth = document.getElementById("currentHealth");
        currentHealth.innerHTML = data;
    });