var selectionControls = [];

  function createSelectionControl(config) {
    var select = document.getElementById(config.selectId);
    var selectedContainer = document.getElementById(config.containerId);
    var feedback = document.getElementById(config.feedbackId);
    var selectedItems = [];

    if (!select || !selectedContainer) {
      return null;
    }

    function updateValidity() {
      var isValid = selectedItems.length > 0;

      feedback.style.display =
        isValid ? "none" : "block";

      select.setCustomValidity(
        isValid ? "" : "Please select at least one item"
      );
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

        removeButton.addEventListener("click", function () {
          selectedItems = selectedItems.filter(function (selectedItem) {
            return selectedItem !== item;
          });
          render();
        });

        selectedBox.appendChild(removeButton);
        selectedContainer.appendChild(selectedBox);
      });
    }

    select.addEventListener("change", function () {
      var value = select.value;

      if (selectedItems.indexOf(value) !== -1) {
        alert(value + " is already selected!");
        select.value = "";
        return;
      }

      selectedItems.push(value);
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

  var form = document.getElementById("movieForm");

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
      selectionControls.forEach(function (control) {
        control.validate();
      });
      form.classList.add("was-validated");
      if (!form.checkValidity()) {
        event.preventDefault();
      }
    });
  }

  var modalTitle = document.getElementById("modalMovieTitle");
  var modalDescription = document.getElementById("modalMovieDescription");
  var modalGenre = document.getElementById("modalMovieGenre");
  var modalProducer = document.getElementById("modalMovieProducer");
  var modalYear = document.getElementById("modalMovieYear");
  var modalActors = document.getElementById("modalMovieActors");

  function fillMovieModal(card) {
    modalTitle.textContent =
      card.querySelector("h5").textContent.trim();
    modalDescription.textContent =
      card.querySelector(".card-text").textContent.trim();
    modalGenre.textContent =
      card.dataset.genre;
    modalProducer.textContent =
      card.dataset.producer;
    modalYear.textContent =
      card.dataset.year;
    modalActors.textContent =
      card.dataset.actors;
  }

  var exploreButtons = document.querySelectorAll(
    'button[data-target="#movieModal"]',
  );
  exploreButtons.forEach(function (button) {
    button.addEventListener("click", function () {
      var card = button.closest(".card").querySelector(".card-body");
      fillMovieModal(card);
    });
  });
  function clear(modalId, prefix) {
  document.getElementById(prefix + "Name").value = "";
  document.getElementById(prefix + "DateOfBirth").value = "";
  document.getElementById(prefix + "Bio").value = "";
  document.getElementById(prefix + "Gender").value = "";
}

document.getElementById("saveProducer").addEventListener("click", function () {
  clear("producerModal", "producer");
});

document.getElementById("saveActor").addEventListener("click", function () {
  clear("actorModal", "actor");
});