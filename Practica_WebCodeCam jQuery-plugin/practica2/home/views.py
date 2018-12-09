#encoding:utf-8
from django.shortcuts import render,redirect
from django.core.urlresolvers import reverse

''' 
En esta practica se utiliza WebCodeCam jQuery-Plugin
se puede encontrar en https://github.com/andrastoth/WebCodeCam.

El fin de esta practica es detectar cierto codigo de barra en especifico y hacer redirect
a la plantilla correspondiente.
'''

# vista. support.html
def support(request):
	barcode=request.session['temp_barcode']
	return render(request, "home/support.html",{
    	'barcode':barcode
    	})



def webcam(request):
    if request.POST:
        #datos que recibe del input id=barcode en la plantilla webcam_template.html
        #para este ejemplo se uso sessions
        request.session['temp_barcode'] = request.POST['barcode']
        barcode=request.session['temp_barcode']
        #Agregar condiciones dependiendo del uso que se le dara con los codigos detectados
        if barcode == '0025215670039':
        	request.session['temp_barcode'] = barcode
        	# MANDAR AL FORMULARIO QUE CORRESPONDA A ESE CODIGO DE BARRA
        	return redirect(reverse('support'))
        # SI EL CODIGO DE BARRA NO COINCIDE CON LA CONDICION TE MANDA A LO SIGUIENTE
    	else:
    		msj_error="Intente de nuevo, una vez detectado el código..."
    		return render(request,"home/webcam_template.html",{'msj_error':msj_error})
    else:# Este else es cuando entras por primera vez a esa pagina y no tienes ningun post de codigo de barra.
    	return render(request,"home/webcam_template.html",{})
