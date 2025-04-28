document.addEventListener("DOMContentLoaded", function () {
    updateCalendar();
    pullDropdownMenu();
    changeSlides();
    executeDarkModeBtn();
    addFixturesDynamicByLeague();
    addFixturesDynamicByDate();
    loadTodayMatches();
    showGridTable();
    showMatchGridLink();
    unfoldSearchBar();
    /*getOptionsForTeamAndReferee();*/
});


// Обновява календара с текущата седмица
function updateCalendar() {
    const days = document.querySelectorAll('.day');
    const today = new Date();
    const startOfWeek = new Date(today);
    startOfWeek.setDate(today.getDate() - (today.getDay() === 0 ? 6 : today.getDay() - 1));

    days.forEach((day, index) => {
        const date = new Date(startOfWeek);
        date.setDate(startOfWeek.getDate() + index);

        const dateElement = day.querySelector('.date');
        if (!dateElement) return;

        if (
            date.getDate() === today.getDate() &&
            date.getMonth() === today.getMonth() &&
            date.getFullYear() === today.getFullYear()
        ) {
            dateElement.textContent = '(today)';
        } else {
            dateElement.textContent = `${date.getDate()}.${date.getMonth() + 1}`;
        }
    });
}

// Показва/скрива падащото меню
function pullDropdownMenu() {
    const button = document.querySelector('.dropdown-button');
    const dropdownContent = document.querySelector('.dropdown-content');

    if (button && dropdownContent) {
        button.addEventListener('click', function () {
            dropdownContent.style.display = dropdownContent.style.display === 'block' ? 'none' : 'block';
        });
    }
}

// Управление на слайдшоу
function changeSlides() {
    const slides = document.getElementById('slides');
    const totalSlides = document.querySelectorAll('.slide').length;
    let index = 0;

    if (!slides) return;

    const updateSlidePosition = () => {
        slides.style.transform = `translateX(-${index * 100}%)`;
    };

    const nextSlide = () => {
        index = (index + 1) % totalSlides;
        updateSlidePosition();
    };

    const prevSlide = () => {
        index = (index - 1 + totalSlides) % totalSlides;
        updateSlidePosition();
    };

    document.getElementById('next')?.addEventListener('click', nextSlide);
    document.getElementById('prev')?.addEventListener('click', prevSlide);

    setInterval(nextSlide, 7000);
}

// Тъмен режим
function executeDarkModeBtn() {
    const toggle = document.getElementById("dark-mode-toggle");
    const leftSide = document.querySelector(".left-side");
    const center = document.querySelector(".center");
    const rightSide = document.querySelector(".right-side");

    if (!toggle || !leftSide || !center || !rightSide) return;

    const setDarkMode = (enabled) => {
        const bgColor = enabled ? "#000" : "white";
        leftSide.style.backgroundColor = bgColor;
        center.style.backgroundColor = bgColor;
        rightSide.style.backgroundColor = bgColor;
    };

    if (localStorage.getItem("darkMode") === "enabled") {
        setDarkMode(true);
        toggle.checked = true;
    }

    toggle.addEventListener("change", function () {
        const enabled = toggle.checked;
        setDarkMode(enabled);
        localStorage.setItem("darkMode", enabled ? "enabled" : "disabled");
    });
}

// Зарежда мачове по лига
async function addFixturesDynamicByLeague() {
    document.querySelectorAll("[data-league]").forEach(leagueElement => {
        leagueElement.addEventListener("click", function (e) {
            e.preventDefault();
            const league = this.getAttribute("data-league");
            const fixturesContainer = document.querySelector(".fixtures");

            if (!fixturesContainer) return;

            if (league === "Champions League") {
                fetch("/Fixture/ChampionsLeagueMatches")
                    .then(response => {
                        if (!response.ok) throw new Error("Грешка при API заявката.");
                        return response.text();
                    })
                    .then(html => fixturesContainer.innerHTML = html)
                    .catch(error => {
                        console.error(error);
                        fixturesContainer.innerHTML = "<p>Грешка при зареждане на данни.</p>";
                    });
            } else {
                fetch('/Fixture/GetFixturesByLeague', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    body: `league=${encodeURIComponent(league)}`
                })
                    .then(response => response.text())
                    .then(html => fixturesContainer.innerHTML = html)
                    .catch(error => {
                        console.error(error);
                        fixturesContainer.innerHTML = "<p>Грешка при зареждане на данни от базата.</p>";
                    });
            }
        });
    });
}

// Зарежда мачове по дата
 function addFixturesDynamicByDate() {
    document.querySelectorAll('.day').forEach(button => {
        button.onclick = async () => {
            const date = button.dataset.date;

            try {
                const response = await fetch('/Fixture/GetFixturesByDate', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ date })
                });

                const html = await response.text();
                document.querySelector('.fixtures').innerHTML = html;
            } catch (err) {
                console.error('Error:', err);
            }
        };
    });
}

// Зарежда днешните мачове
function loadTodayMatches() {
    const container = document.getElementById('matches-container');
    if (!container) {
        console.error('matchesContainer не е намерен в DOM!');
        return;
    }

    fetch('/Fixture/TodayMatches')
        .then(response => {
            if (!response.ok) throw new Error('Мрежова грешка.');
            return response.text();
        })
        .then(html => container.innerHTML = html)
        .catch(error => {
            container.innerHTML = '<p>Възникна грешка при зареждането.</p>';
            console.error(error);
        });
}

// Показва мачовете в таблица по лига
function showGridTable() {
    document.querySelectorAll('.league').forEach(button => {
        button.addEventListener('click', function () {
            const league = this.getAttribute('data-league');
            const matchTable = document.getElementById('match-table');
            if (!matchTable) return;

            fetch(`/Fixture/MatchGrid?league=${encodeURIComponent(league)}`)
                .then(response => response.text())
                .then(html => matchTable.innerHTML = html)
                .catch(err => console.error('Error loading match grid:', err));
        });
    });
}

// Показва динамичен линк за таблицата
function showMatchGridLink() {
    document.querySelectorAll(".league").forEach(button => {
        button.addEventListener("click", function () {
            const league = this.getAttribute("data-league");
            const container = document.getElementById("grid-link-container");
            if (!container) return;

            const link = document.createElement("a");
            link.href = `/Fixture/MatchGrid?league=${encodeURIComponent(league)}`;
            link.textContent = `Виж таблицата за ${league}`;
            link.target = "_blank";
            link.style.display = "inline-block";
            link.style.marginTop = "10px";
            link.style.fontWeight = "bold";

            container.innerHTML = "";
            container.appendChild(link);
        });
    });
}

// Търсене с предложения
async function searchSuggestions() {
    const query = document.getElementById('search-input').value.trim();
    const suggestionsContainer = document.getElementById('suggestions-container');

    if (query.length < 2) {
        suggestionsContainer.style.display = 'none';
        return;
    }

    const response = await fetch(`/api/search?query=${encodeURIComponent(query)}`);
    const suggestions = await response.json();

    suggestionsContainer.innerHTML = '';

    if (suggestions.length > 0) {
        suggestions.forEach(suggestion => {
            const div = document.createElement('div');
            div.textContent = suggestion.name;
            div.onclick = function () {
                document.getElementById('search-input').value = suggestion.name;
                document.getElementById('team-id').value = suggestion.id;
                suggestionsContainer.style.display = 'none';
            };
            suggestionsContainer.appendChild(div);
        });
        suggestionsContainer.style.display = 'block';
    } else {
        suggestionsContainer.style.display = 'none';
    }
}

// Анимация на търсачката
function unfoldSearchBar() {
    const input = document.getElementById('search-input');
    const box = input?.closest('.box');
    if (!input || !box) return;

    input.addEventListener('input', () => {
        box.classList.toggle('active', input.value.trim().length > 0);
    });

    input.addEventListener('focus', () => {
        box.classList.add('active');
    });

    input.addEventListener('blur', () => {
        setTimeout(() => {
            if (input.value.trim() === '') {
                box.classList.remove('active');
            }
        }, 150);
    });
}
