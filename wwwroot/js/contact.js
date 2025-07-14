document.addEventListener('DOMContentLoaded', function () {
    const select = document.getElementById('countrySelect');

    fetch('https://restcountries.com/v3.1/all?fields=name')
        .then(response => response.json())
        .then(data => {
            // Sort alphabetically by country name
            const countries = data
                .map(country => country.name.common)
                .sort((a, b) => a.localeCompare(b));

            // Clear existing options
            select.innerHTML = '';

            // Add "Select Country*" as first option
            const defaultOption = document.createElement('option');
            defaultOption.value = '';
            defaultOption.textContent = 'Select Country*';
            select.appendChild(defaultOption);

            // Add countries as options
            countries.forEach(name => {
                const option = document.createElement('option');
                option.value = name;
                option.textContent = name;
                // Pre-select Nepal
                if (name === 'Nepal') {
                    option.selected = true;
                }
                select.appendChild(option);
            });
        })
        .catch(error => {
            console.error('Error fetching countries:', error);
            // Fallback to Nepal if load fails
            select.innerHTML = `
            <option value="">Select Country*</option>
            <option value="Nepal" selected>Nepal</option>
          `;
        });
});

// Always hide form on page load
window.addEventListener('DOMContentLoaded', function () {
    const section = document.getElementById('contactFormSection');
    if (section) {
        section.style.display = 'none';
    }
});

function toggleContactForm() {
    const section = document.getElementById('contactFormSection');
    if (section) {
        if (section.style.display === 'none' || section.style.display === '') {
            section.style.display = 'block';
            section.scrollIntoView({ behavior: 'smooth' });
        } else {
            section.style.display = 'none';
        }
    }
}