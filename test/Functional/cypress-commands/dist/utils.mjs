// https://stackoverflow.com/questions/105034/how-to-create-guid-uuid

function generateUniqueName() {
    var date = new Date();
    var today = new Date(date.getFullYear(), date.getMonth(), date.getDay());
    return 't' + Math.abs(date - today).toString(32);
}

function generateTenantInfo(setupRecipeName, description) {
  var uniqueName = generateUniqueName();
  return {
      name: uniqueName,
      prefix: uniqueName,
      setupRecipe: setupRecipeName,
      description
  }
}

export { generateTenantInfo };
