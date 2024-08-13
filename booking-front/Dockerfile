FROM node:20

RUN npm install -g @angular/cli

WORKDIR /angular-app

COPY package.json package-lock.json ./

RUN npm install

COPY . .

EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200"]