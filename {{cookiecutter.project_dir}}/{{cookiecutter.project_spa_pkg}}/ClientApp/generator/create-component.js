// npm run generate component 
 

require("colors");
const fs = require("fs");
const templates = require("./templates/component");

const componentName = process.argv[2];

if (!componentName) {
  console.error("Please supply a valid component name".red);
  process.exit(1);
}

console.log("Creating Component Templates with name: " + componentName);

const componentDirectory = `./src/${componentName}`;

if (fs.existsSync(componentDirectory)) {
  console.error(`Component ${componentName} already exists.`.red);
  process.exit(1);
}

fs.mkdirSync(componentDirectory);

const generatedTemplates = templates.map((template) => template(componentName));

generatedTemplates.forEach((template) => {

  if(template.extension == '.scss'){
   
  } else{
    var fullPath = `${componentDirectory}/${componentName}${template.extension}`;

    if(template.extension == '.tsx'){
      fullPath = `${componentDirectory}/index${template.extension}`;
    }
  
    console.log(
      "File name: " + fullPath
    );
  
    fs.writeFileSync(
      fullPath,
      template.content
    );
  }

  
});

console.log(
  "Successfully created component under: " + componentDirectory.green
);
