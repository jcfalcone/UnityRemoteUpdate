'use strict';

class ObjHolder extends React.Component 
{
  constructor(props) 
  {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      Rotation: [],
      Color: []
    };
  }

  componentDidMount()
  {
    fetch("ObjData.php")
      .then(res => res.json())
      .then(
        (result) => {
          this.setState({
            isLoaded: true,
            Rotation: result.Rotation,
            Color: result.Color
          });
        },
        // Note: it's important to handle errors here
        // instead of a catch() block so that we don't swallow
        // exceptions from actual bugs in components.
        (error) => {
          this.setState({
            isLoaded: true,
            error
          });
        }
      );
  }

  UpdateSpeed()
  {
    var speedX = document.querySelector('#speedX');
    speedX = speedX.value;

    var speedY = document.querySelector('#speedY');
    speedY = speedY.value;

    var speedZ = document.querySelector('#speedZ');
    speedZ = speedZ.value;
  
    
    console.log(fetch('UpdateData.php', {
        method: "POST",
        headers: 
        {
            "Content-Type": "application/json; charset=utf-8",
            // "Content-Type": "application/x-www-form-urlencoded",
        },
        body: JSON.stringify({SpeedX : speedX, SpeedY : speedY, SpeedZ : speedZ}), // body data type must match "Content-Type" header
    })
    .then(response => response)); // parses response to JSON
  }

  render() 
  {
    if(this.state.error)
    {
      return e('div', {}, 'Error: '+this.state.error.message);
    }


    if(!this.state.isLoaded)
    {
      return e('div', {}, 'Loading...');
    }

    return e
    (
      'div',

      { className: 'Holder' },
      
      e('div', { className: 'topTitle' }, 'Color'),
      e('div', { className: 'ColorPicker' }, e(ColorPicker, {colorR : this.state.Color[0], colorG : this.state.Color[1], colorB : this.state.Color[2], alpha : this.state.Color[3]})),
      e('div', { className: 'Title' }, 'Speed'),
      e('div', { className: 'Speed' }, e(SpeedInput, {label: 'X', value: this.state.Rotation[0], parent : this}), e(SpeedInput, {label: 'Y', value: this.state.Rotation[1], parent : this}), e(SpeedInput, {label: 'Z', value: this.state.Rotation[2], parent : this})),
      e('div', { className: 'StatusBar' }, '')
    );
  }
}

const domContainer = document.querySelector('#like_button_container');
ReactDOM.render(e(ObjHolder), domContainer);