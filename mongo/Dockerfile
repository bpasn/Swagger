FROM mongo:4.4

ENV MONGO_ROOT_USER=root \
    MONGO_ROOT_PWD=1234 \
    MONGO_DB=swagger \
    MONGO_USER=swagger \
    MONGO_PWD=1234

COPY data /data/db

COPY init.sh /docker-entrypoint-initdb.d/init-mongo.sh
VOLUME [ "/data:" , "/data/db" ]