// npm run generate:slice 
// npm run generate:slice "some/path"
require("colors");
const fs = require("fs");
const templates = require("./templates/slice");

const sliceName = process.argv[2];
const storePath =  `./src/store`;
const slicePath =  `${storePath}/slices`;
const relStorePath = (process.argv.length > 3) ? `${process.argv[3]}` : `../../../../store`;

if (!sliceName) {
  console.error("Please supply a valid component slice name".red);
  process.exit(1);
}
console.log( `Store Path: ${storePath.blue}`);
console.log( `Slice(s) Path: ${slicePath.blue}`); 
console.log(`Slice Name: ${(sliceName.charAt(0).toLowerCase() + sliceName.slice(1)).blue}\n`);

const sliceDirectory = `${slicePath}/${sliceName.charAt(0).toLowerCase() + sliceName.slice(1)}`;

if (fs.existsSync(sliceDirectory)) {
  fs.rmdirSync(sliceDirectory, { recursive: true })
  console.error(`[${sliceName}] directory already exists. Overwriting...`.bgRed + '\n');
  //process.exit(1);
}

fs.mkdirSync(sliceDirectory, { recursive: true }, (err) => {
  if(err){
    return console.error(err).bgRed; 
  }
  console.log(`Directory Created: ${sliceDirectory.green}`); 
}); 
const generatedTemplates = templates.map((template) => template(sliceName, relStorePath));

generatedTemplates.forEach((template) => { 
  // Exclusions
  if(template.extension == '.exclude'){
   
  } else{
    var fullPath = `${sliceDirectory}/${sliceName.charAt(0).toLowerCase() + sliceName.slice(1)}${template.extension}`; 

    // For root index
    if(template.extension == '.ts'){
      fullPath = `${sliceDirectory}/index${template.extension}`;
    }

    console.log( `Generated: ${fullPath.yellow}`);
  
    fs.writeFileSync(
      fullPath,
      template.content
    );
  } 
});

console.log( `\n🍺 🍺  Successfully created slice at: ${sliceDirectory.green}\n`);
