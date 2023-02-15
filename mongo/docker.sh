echo -e "Create Contailer...\n"
docker run -i -d \
    -v "data:/data/db" \
    -v "init.sh:/docker-entrypoint-initdb.d/init.sh" \
    -p "27018:27017" \
    --name swagger-mongo \
    swagger-mongo
echo -e "Create Container Successfully."
exit 1