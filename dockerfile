# Stage:1 to build the code.
FROM node:20-alpine AS build
WORKDIR /project
COPY *.json ./
RUN npm install
COPY . .
RUN npm run build --configuration=production

# Stage 2 – copy compiled files and serve with NGINX
FROM nginx:alpine

# copy custom nginx config (see above)
COPY nginx.conf /etc/nginx/conf.d/default.conf

# copy built Angular files into NGINX’s html directory; adjust path if angular.json changes
COPY --from=build /project/dist/* /usr/share/nginx/html/

# expose HTTP port
EXPOSE 80

# start NGINX in the foreground
CMD ["nginx", "-g", "daemon off;"]
