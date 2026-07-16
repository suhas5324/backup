export default function createMovieFormData(movie) {
  const formData = new FormData();

  formData.append("Name", movie.name);
  formData.append("YearOfRelease", movie.yearOfRelease);
  formData.append("Plot", movie.plot);
  formData.append("ProducerId", movie.producerId);

  movie.actorIds.forEach((id) => formData.append("actorIds", id));
  movie.genreIds.forEach((id) => formData.append("genreIds", id));

  if (movie.coverImage) {
    formData.append("CoverImage", movie.coverImage);
  }

  return formData;
}
