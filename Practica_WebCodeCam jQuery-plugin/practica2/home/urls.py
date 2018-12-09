from django.conf.urls import url
from . import views

urlpatterns=[
	url(r'^support/$',views.support,name='support'),
	url(r'^$',views.webcam,name='webcam')
]