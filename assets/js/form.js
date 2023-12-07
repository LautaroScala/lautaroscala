document.getElementById("contactForm").addEventListener("submit", function(event) {
    event.preventDefault();
    
    var formData = new FormData(event.target);

    var email = document.getElementById("email").value;

    // Validar formato de correo electrónico 
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      alert("Por favor, ingresa un correo electrónico válido.");
      event.preventDefault();
      return false;
    }
    fetch('https://formspree.io/f/mrgwraoq', {
        method: 'POST',
        body: formData,
        headers: {
            'Accept':'application/json'
        }
      })
      .then(response => response.json())
      .then(data => {
        alert("Enviado con exito! Gracias por contactarme!")
      })
      .catch(error => {
        alert("¡Algo salio mal! Intenta más tarde")
      })
  });