'use strict';

const e = React.createElement;

class SpeedInput extends React.Component 
{
  constructor(props) 
  {
    super(props);
  }

  handleChangeField(e)
  {
    
  	var obj = {};
	obj[e.target.id] = e.target.value;

    console.log(fetch('UpdateData.php', {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json; charset=utf-8",
            // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify(obj), // body data type must match "Content-Type" header
    })
    .then(response => response)); // parses response to JSON
  }

  render() 
  {
    return e('div', {}, e('label', {}, this.props.label), e('input', {id : "Speed"+this.props.label, type:"number", className:"NumberInput", defaultValue: this.props.value, onChange : this.handleChangeField}));
  }
}