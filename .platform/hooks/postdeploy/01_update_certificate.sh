#!/bin/bash

DOMAIN="e-commerce.ap-south-1.elasticbeanstalk.com"
EMAIL="ameer.yaish@iih.digital"

if command -v certbot &> /dev/null
then
    sudo certbot --nginx -d $DOMAIN --non-interactive --agree-tos --email $EMAIL --redirect
    if [ $? -ne 0 ]; then
        echo "Certbot failed to generate the certificate for $DOMAIN"
        exit 1
    fi
    sudo systemctl reload nginx
else
    echo "Certbot is not installed"
    exit 1
fi
