'use strict';

//Function to convert hex format to a rgb color
function rgb2hex(r, g, b)
{
  return  "#" +
        ("0" + parseInt(r,10).toString(16)).slice(-2) +
        ("0" + parseInt(g,10).toString(16)).slice(-2) +
        ("0" + parseInt(b,10).toString(16)).slice(-2);
}

function hexToRgb(hex) {
  var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
  return result ? {
      r: parseInt(result[1], 16),
      g: parseInt(result[2], 16),
      b: parseInt(result[3], 16)
  } : null;
}

class ColorPicker extends React.Component 
{
  constructor(props) 
  {
    super(props);
  }

  handleChangeColor(e)
  {
    var alpha = document.querySelector('#colorAlpha');
    var colors = hexToRgb(e.target.value);
    
    console.log(fetch('UpdateData.php', {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json; charset=utf-8",
            // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify({ColorR : colors.r, ColorG : colors.g, ColorB : colors.b, ColorAlpha : alpha.value}), // body data type must match "Content-Type" header
    })
    .then(response => response)); // parses response to JSON
  }

  handleChangeAlpha(e)
  {
    var alpha = e.target.value;
    var colors = document.querySelector('#colorPicker');
    colors = hexToRgb(colors.value);

    console.log(alpha);
  
    
    console.log(fetch('UpdateData.php', {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json; charset=utf-8",
            // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify({ColorR : colors.r, ColorG : colors.g, ColorB : colors.b, ColorAlpha : alpha}), // body data type must match "Content-Type" header
    })
    .then(response => response)); // parses response to JSON
  }

  render() 
  {
    return e('div', {}, 
              e('div', {}, 
                e('label', {}, 'Color'), 
                e('input', {id : "colorPicker", type:'color', defaultValue: rgb2hex(this.props.colorR, this.props.colorG, this.props.colorB), onChange : this.handleChangeColor}), 
                e('label', {}, 'Alpha'), 
                e('input', {id: "colorAlpha", type:"range", min:"0", max:"255", defaultValue: this.props.alpha, onChange : this.handleChangeAlpha})));
  }
}