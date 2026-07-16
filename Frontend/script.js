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

    feedback.style.display = isValid ? "none" : "block";

    select.setCustomValidity(isValid ? "" : "Please select at least one item");
  }

function render() {
    selectedContainer.innerHTML = "";
    var wrapper = selectedContainer.closest(".selection-input");

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

    if (wrapper) {
      wrapper.classList.toggle("has-chips", selectedItems.length > 0);
    }
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
var posterFeedback = document.querySelector(
  '#moviePoster ~ .invalid-feedback'
);
  form.addEventListener("submit", function (event) {
     var textFields = form.querySelectorAll(
      'input[type="text"], input[type="number"], textarea'
    );
    textFields.forEach(function (field) {
      field.value = field.value.replace(/\s+$/, "");
    });
    selectionControls.forEach(function (control) {
      control.validate();
    });
    if (posterFeedback) {
  posterFeedback.style.display = posterInput.files.length
    ? "none"
    : "block";
}
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
  modalTitle.textContent = card.querySelector("h5").textContent.trim();
  modalDescription.textContent = card
    .querySelector(".card-text")
    .textContent.trim();
  modalGenre.textContent = card.dataset.genre;
  modalProducer.textContent = card.dataset.producer;
  modalYear.textContent = card.dataset.year;
  modalActors.textContent = card.dataset.actors;
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
var personModalTitle = document.getElementById("personModalTitle");
var addActorBtn = document.getElementById("addActorBtn");
var addProducerBtn = document.getElementById("addProducerBtn");

if (personModalTitle && addActorBtn && addProducerBtn) {
  addActorBtn.addEventListener("click", function () {
    personModalTitle.textContent = "Actor Details";
  });

  addProducerBtn.addEventListener("click", function () {
    personModalTitle.textContent = "Producer Details";
  });
}

function clearPersonForm() {
  var personForm = document.getElementById("personForm");

  if (personForm) {
    personForm.reset();
    personForm.classList.remove("was-validated");
  }
}

var savePersonButton = document.getElementById("savePerson");
var personForm = document.getElementById("personForm");

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

var posterInput = document.getElementById("moviePoster");
var posterPreview = document.getElementById("posterPreview");

if (posterInput && posterPreview) {
  posterInput.addEventListener("change", function () {
    var file = posterInput.files[0];

    if (!file || !file.type.startsWith("image/")) {
      posterPreview.src = "";
      posterPreview.classList.add("d-none");
      return;
    }

    posterPreview.src = URL.createObjectURL(file);
    posterPreview.classList.remove("d-none");
  });
}