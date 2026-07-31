(function () {
  "use strict";

  const TITLE_MAX_LENGTH = 30;
  const DESCRIPTION_MAX_LENGTH = 90;
  const ALLOWED_POSTER_TYPES = ["image/jpeg", "image/png"];

  const movies = [
    {
      title: "Interstellar",
      description: "A science fiction movie about space exploration.",
      poster: "Poster-Images/interstellar.png",
      genre: "Sci-Fi",
      producer: "Christopher Nolan",
      year: "2014",
      actors: ["Matthew McConaughey", "Anne Hathaway", "Jessica Chastain"],
    },
    {
      title: "Inception",
      description: "A movie based on dreams and mind bending concepts.",
      poster: "Poster-Images/inception.jpg",
      genre: "Sci-Fi",
      producer: "Emma Thomas",
      year: "2010",
      actors: ["Leonardo DiCaprio", "Joseph Gordon-Levitt", "Ellen Page"],
    },
    {
      title: "Avatar",
      description: "A visually stunning adventure movie.",
      poster: "Poster-Images/avatar.png",
      genre: "Fantasy",
      producer: "James Cameron",
      year: "2009",
      actors: ["Sam Worthington", "Zoe Saldana", "Sigourney Weaver"],
    },
    {
      title: "Titanic",
      description: "A romantic movie set on a famous ship.",
      poster: "Poster-Images/titanic.png",
      genre: "Romance",
      producer: "James Cameron",
      year: "1997",
      actors: ["Leonardo DiCaprio", "Kate Winslet", "Billy Zane"],
    },
    {
      title: "Joker",
      description: "A psychological thriller movie.",
      poster: "Poster-Images/joker.jpg",
      genre: "Drama",
      producer: "Todd Phillips",
      year: "2019",
      actors: ["Joaquin Phoenix", "Robert De Niro", "Zazie Beetz"],
    },
    {
      title: "The Batman",
      description: "A dark superhero action movie.",
      poster: "Poster-Images/batman.jpg",
      genre: "Action",
      producer: "Dylan Clark",
      year: "2022",
      actors: ["Robert Pattinson", "Zoë Kravitz", "Colin Farrell"],
    },
    {
      title: "Avengers",
      description: "Marvel superheroes save the world.",
      poster: "Poster-Images/avengers.jpg",
      genre: "Action",
      producer: "Kevin Feige",
      year: "2012",
      actors: ["Robert Downey Jr", "Chris Evans", "Scarlett Johansson"],
    },
    {
      title: "Gladiator",
      description: "A warrior fights for justice.",
      poster: "Poster-Images/gladiator.jpg",
      genre: "Drama",
      producer: "Douglas Wick",
      year: "2000",
      actors: ["Russell Crowe", "Joaquin Phoenix", "Connie Nielsen"],
    },
    {
      title: "Dune",
      description: "An epic science fiction adventure.",
      poster: "Poster-Images/dune.jpg",
      genre: "Sci-Fi",
      producer: "Mary Parent",
      year: "2021",
      actors: ["Timothée Chalamet", "Zendaya", "Rebecca Ferguson"],
    },
    {
      title: "Frozen",
      description: "An animated fantasy musical movie.",
      poster: "Poster-Images/frozen.jpg",
      genre: "Animation",
      producer: "Peter Del Vecho",
      year: "2013",
      actors: ["Idina Menzel", "Kristen Bell", "Jonathan Groff"],
    },
    {
      title: "Doctor Strange",
      description:
        "A movie about magic and multiverse. Lorem ipsum dolor sit amet consectetur adipisicing elit. Perspiciatis modi, nobis reprehenderit sed ad maiores ea in ut officiis cupiditate dignissimos neque similique recusandae suscipit distinctio aut quisquam mollitia consectetur.",
      poster: "Poster-Images/strange.jpg",
      genre: "Action",
      producer: "Kevin Feige",
      year: "2016",
      actors: ["Benedict Cumberbatch", "Chiwetel Ejiofor", "Rachel McAdams"],
    },
    {
      title: "Iron Man",
      description: "Story of a genius billionaire superhero.",
      poster: "Poster-Images/iron.jpg",
      genre: "Action",
      producer: "Avi Arad",
      year: "2008",
      actors: ["Robert Downey Jr", "Gwyneth Paltrow", "Terrence Howard"],
    },
  ];

  function truncateText(text, maxLength) {
    if (!text || text.length <= maxLength) {
      return text;
    }
    return text.slice(0, maxLength).trim() + "…";
  }

  const modalTitle = document.getElementById("modalMovieTitle");
  const modalDescription = document.getElementById("modalMovieDescription");
  const modalGenre = document.getElementById("modalMovieGenre");
  const modalProducer = document.getElementById("modalMovieProducer");
  const modalYear = document.getElementById("modalMovieYear");
  const modalActors = document.getElementById("modalMovieActors");

  function fillMovieModal(movie) {
    modalTitle.textContent = movie.title;
    modalDescription.textContent = movie.description;
    modalGenre.textContent = movie.genre;
    modalProducer.textContent = movie.producer;
    modalYear.textContent = movie.year;
    modalActors.textContent = movie.actors.join(", ");
  }

  function createMovieCard(movie) {
    const col = document.createElement("div");
    col.className = "col-12 col-md-3 mb-4";

    const card = document.createElement("div");
    card.className = "card h-100 shadow-sm";

    const img = document.createElement("img");
    img.src = movie.poster;
    img.className = "card-img";
    img.alt = movie.title;

    const cardBody = document.createElement("div");
    cardBody.className = "card-body d-flex flex-column";

    const title = document.createElement("h5");
    title.className = "font-weight-bold";
    title.textContent = truncateText(movie.title, TITLE_MAX_LENGTH);
    title.title = movie.title;

    const description = document.createElement("p");
    description.className = "card-text";
    description.textContent = truncateText(
      movie.description,
      DESCRIPTION_MAX_LENGTH,
    );
    description.title = movie.description;

    const actions = document.createElement("div");
    actions.className = "mt-auto";

    const exploreBtn = document.createElement("button");
    exploreBtn.type = "button";
    exploreBtn.setAttribute("data-toggle", "modal");
    exploreBtn.setAttribute("data-target", "#movieModal");
    exploreBtn.innerHTML = '<i class="bi bi-eye"></i>';
    exploreBtn.addEventListener("click", function () {
      fillMovieModal(movie);
    });

    const editBtn = document.createElement("button");
    editBtn.type = "button";
    editBtn.innerHTML = '<i class="bi bi-pencil"></i>';

    const deleteBtn = document.createElement("button");
    deleteBtn.type = "button";
    deleteBtn.innerHTML = '<i class="bi bi-trash"></i>';

    actions.appendChild(exploreBtn);
    actions.appendChild(editBtn);
    actions.appendChild(deleteBtn);

    cardBody.appendChild(title);
    cardBody.appendChild(description);
    cardBody.appendChild(actions);

    card.appendChild(img);
    card.appendChild(cardBody);
    col.appendChild(card);

    return col;
  }

  function renderMovieList() {
    const movieList = document.getElementById("movieList");

    if (!movieList) {
      return;
    }

    movies.forEach(function (movie) {
      movieList.appendChild(createMovieCard(movie));
    });
  }

  function createSelectionControl(config) {
    const select = document.getElementById(config.selectId);
    const selectedContainer = document.getElementById(config.containerId);
    const feedback = document.getElementById(config.feedbackId);
    let selectedItems = [];

    if (!select || !selectedContainer) {
      return null;
    }

    function updateValidity() {
      const isValid = selectedItems.length > 0;
      const wrapper = selectedContainer.closest(".selection-input");

      feedback.style.display = isValid ? "none" : "block";
      if (wrapper) {
        wrapper.classList.toggle("is-invalid", !isValid);
      }

      select.setCustomValidity(isValid ? "" : "Please select at least one item");
    }

function render() {
      selectedContainer.innerHTML = "";
      const wrapper = selectedContainer.closest(".selection-input");

      selectedItems.forEach(function (item) {
        const selectedBox = document.createElement("span");
        selectedBox.className = "selected-item-box";
        selectedBox.textContent = item;
        selectedContainer.appendChild(selectedBox);
      });

      if (wrapper) {
        wrapper.classList.toggle("has-chips", selectedItems.length > 0);
      }

      // Highlight already-selected options in the dropdown list
      Array.from(select.options).forEach(function (option) {
        option.classList.toggle(
          "option-selected",
          selectedItems.indexOf(option.value) !== -1,
        );
      });
    }

    // Selecting an already-selected item toggles it off instead of alerting
    select.addEventListener("change", function () {
      const value = select.value;

      if (!value) {
        return;
      }

      const existingIndex = selectedItems.indexOf(value);

      if (existingIndex !== -1) {
        selectedItems.splice(existingIndex, 1);
      } else {
        selectedItems.push(value);
      }

      updateValidity();
      select.value = "";
      render();
    });

    return {
      validate: function () {
        updateValidity();
      },
    };
  }

  const selectionControls = [];
  const form = document.getElementById("movieForm");

  if (form) {
    selectionControls.push(
      createSelectionControl({
        selectId: "actorSelect",
        containerId: "selectedActors",
        feedbackId: "actorListFeedback",
      }),
      createSelectionControl({
        selectId: "genreSelect",
        containerId: "selectedGenres",
        feedbackId: "genreListFeedback",
      }),
    );

    form.addEventListener("submit", function (event) {
      const textFields = form.querySelectorAll(
        'input[type="text"], input[type="number"], textarea',
      );
      textFields.forEach(function (field) {
        field.value = field.value.trim();
      });

      form.classList.add("was-validated");
      selectionControls.forEach(function (control) {
        control.validate();
      });

      if (!form.checkValidity()) {
        event.preventDefault();
      }
    });
  }

  const personModalTitle = document.getElementById("personModalTitle");
  const addActorBtn = document.getElementById("addActorBtn");
  const addProducerBtn = document.getElementById("addProducerBtn");

  if (personModalTitle && addActorBtn && addProducerBtn) {
    addActorBtn.addEventListener("click", function () {
      personModalTitle.textContent = "Actor Details";
    });

    addProducerBtn.addEventListener("click", function () {
      personModalTitle.textContent = "Producer Details";
    });
  }

  function clearPersonForm() {
    const personForm = document.getElementById("personForm");

    if (personForm) {
      personForm.reset();
      personForm.classList.remove("was-validated");
    }
  }

  const savePersonButton = document.getElementById("savePerson");
  const personForm = document.getElementById("personForm");

  if (savePersonButton && personForm) {
    savePersonButton.addEventListener("click", function () {
      personForm.classList.add("was-validated");

      if (!personForm.checkValidity()) {
        return;
      }

      $("#personModal").modal("hide");
      clearPersonForm();
    });
  }

  const posterInput = document.getElementById("moviePoster");
  const posterPreview = document.getElementById("posterPreview");

  if (posterInput && posterPreview) {
    posterInput.addEventListener("change", function () {
      const file = posterInput.files[0];

      if (!file) {
        return;
      }

      if (!ALLOWED_POSTER_TYPES.includes(file.type)) {
        alert("Please upload a poster image in JPG, JPEG or PNG format.");
        posterInput.value = "";
        posterPreview.classList.add("d-none");
        return;
      }

      posterPreview.src = URL.createObjectURL(file);
      posterPreview.classList.remove("d-none");
    });
  }

  renderMovieList();
})();