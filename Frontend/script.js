(function () {
  "use strict";

  var forms = document.querySelectorAll(".needs-validation");
  var selectionControls = [];

  function createSelectionControl(config) {
    var select = document.getElementById(config.selectId);
    var selectedContainer = document.getElementById(config.containerId);
    var hiddenInput = document.getElementById(config.inputId);
    var feedback = document.getElementById(config.feedbackId);
    var selectedItems = [];

    if (!select || !selectedContainer || !hiddenInput) {
      return null;
    }

    function updateValidity(showFeedback) {
      var isValid = selectedItems.length > 0;
      select.setCustomValidity(isValid ? "" : config.validationMessage);

      if (feedback) {
        feedback.style.display = !isValid && showFeedback ? "block" : "none";
      }
    }

    function render() {
      selectedContainer.innerHTML = "";

      selectedItems.forEach(function (item) {
        var selectedBox = document.createElement("span");
        selectedBox.className = "selected-item-box";
        selectedBox.appendChild(document.createTextNode(item));

        var removeButton = document.createElement("button");
        removeButton.type = "button";
        removeButton.className = "selected-item-remove";
        removeButton.textContent = "\u00d7";
        removeButton.setAttribute("aria-label", "Remove " + item);

        removeButton.addEventListener("click", function () {
          selectedItems = selectedItems.filter(function (selectedItem) {
            return selectedItem !== item;
          });

          hiddenInput.value = selectedItems.join(", ");
          updateValidity(
            select.closest("form").classList.contains("was-validated"),
          );
          render();
        });

        selectedBox.appendChild(removeButton);
        selectedContainer.appendChild(selectedBox);
      });
    }

    select.addEventListener("change", function () {
      var value = select.value;

      if (!value) return;

      if (selectedItems.indexOf(value) !== -1) {
        alert(value + " is already selected!");
        select.value = "";
        return;
      }

      selectedItems.push(value);
      select.value = "";
      hiddenInput.value = selectedItems.join(", ");
      updateValidity(false);
      render();
    });

    updateValidity(false);
    render();

    return {
      validate: function () {
        updateValidity(true);
      },
    };
  }

  selectionControls.push(
    createSelectionControl({
      selectId: "actorSelect",
      containerId: "selectedActors",
      inputId: "actorListInput",
      feedbackId: "actorListFeedback",
      validationMessage: "Please add at least one actor.",
    }),
    createSelectionControl({
      selectId: "genreSelect",
      containerId: "selectedGenres",
      inputId: "genreListInput",
      feedbackId: "genreListFeedback",
      validationMessage: "Please add at least one genre.",
    }),
  );

  selectionControls = selectionControls.filter(function (control) {
    return control !== null;
  });

  Array.prototype.slice.call(forms).forEach(function (form) {
    form.addEventListener(
      "submit",
      function (event) {
        selectionControls.forEach(function (control) {
          control.validate();
        });

        if (!form.checkValidity()) {
          event.preventDefault();
          event.stopPropagation();
        }

        form.classList.add("was-validated");
      },
      false,
    );
  });

  var modalTitle = document.getElementById("modalMovieTitle");
  var modalDescription = document.getElementById("modalMovieDescription");
  var modalGenre = document.getElementById("modalMovieGenre");
  var modalProducer = document.getElementById("modalMovieProducer");
  var modalYear = document.getElementById("modalMovieYear");
  var modalActors = document.getElementById("modalMovieActors");

  var movieData = {
    Interstellar: {
      description: "A science fiction movie about space exploration.",
      genre: "Sci-Fi",
      producer: "Christopher Nolan",
      year: "2014",
      actors: ["Matthew McConaughey", "Anne Hathaway", "Jessica Chastain"],
    },
    Inception: {
      description: "A movie based on dreams and mind bending concepts.",
      genre: "Sci-Fi",
      producer: "Emma Thomas",
      year: "2010",
      actors: ["Leonardo DiCaprio", "Joseph Gordon-Levitt", "Ellen Page"],
    },
    Avatar: {
      description: "A visually stunning adventure movie.",
      genre: "Fantasy",
      producer: "James Cameron",
      year: "2009",
      actors: ["Sam Worthington", "Zoe Saldana", "Sigourney Weaver"],
    },
    Titanic: {
      description: "A romantic movie set on a famous ship.",
      genre: "Romance",
      producer: "James Cameron",
      year: "1997",
      actors: ["Leonardo DiCaprio", "Kate Winslet", "Billy Zane"],
    },
    Joker: {
      description: "A psychological thriller movie.",
      genre: "Drama",
      producer: "Todd Phillips",
      year: "2019",
      actors: ["Joaquin Phoenix", "Robert De Niro", "Zazie Beetz"],
    },
    "The Batman": {
      description: "A dark superhero action movie.",
      genre: "Action",
      producer: "Dylan Clark",
      year: "2022",
      actors: ["Robert Pattinson", "Zoë Kravitz", "Colin Farrell"],
    },
    Avengers: {
      description: "Marvel superheroes save the world.",
      genre: "Action",
      producer: "Kevin Feige",
      year: "2012",
      actors: ["Robert Downey Jr", "Chris Evans", "Scarlett Johansson"],
    },
    Gladiator: {
      description: "A warrior fights for justice.",
      genre: "Drama",
      producer: "Douglas Wick",
      year: "2000",
      actors: ["Russell Crowe", "Joaquin Phoenix", "Connie Nielsen"],
    },
    Dune: {
      description: "An epic science fiction adventure.",
      genre: "Sci-Fi",
      producer: "Mary Parent",
      year: "2021",
      actors: ["Timothée Chalamet", "Zendaya", "Rebecca Ferguson"],
    },
    Frozen: {
      description: "An animated fantasy musical movie.",
      genre: "Animation",
      producer: "Peter Del Vecho",
      year: "2013",
      actors: ["Idina Menzel", "Kristen Bell", "Jonathan Groff"],
    },
    "Doctor Strange": {
      description: "A movie about magic and multiverse.",
      genre: "Action",
      producer: "Kevin Feige",
      year: "2016",
      actors: ["Benedict Cumberbatch", "Chiwetel Ejiofor", "Rachel McAdams"],
    },
    "Iron Man": {
      description: "Story of a genius billionaire superhero.",
      genre: "Action",
      producer: "Avi Arad",
      year: "2008",
      actors: ["Robert Downey Jr", "Gwyneth Paltrow", "Terrence Howard"],
    },
  };

  function fillMovieModal(title) {
    if (!modalTitle || !modalDescription || !modalGenre || !modalProducer || !modalYear || !modalActors) {
      return;
    }

    var details = movieData[title];
    modalTitle.textContent = title;
    modalDescription.textContent = details.description;
    modalGenre.textContent = details.genre;
    modalProducer.textContent = details.producer;
    modalYear.textContent = details.year;
    modalActors.textContent = (details.actors || []).join(", ");
  }

  var exploreButtons = document.querySelectorAll('button[data-target="#movieModal"]');

  exploreButtons.forEach(function (button) {
    button.addEventListener("click", function () {
      var card = button.closest(".card-body");
      var titleElement = card && card.querySelector("h5");
      var title = titleElement ? titleElement.textContent.trim() : "";
      fillMovieModal(title);
    });
  });
})();