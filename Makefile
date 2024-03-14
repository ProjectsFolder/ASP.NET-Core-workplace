compose=docker-compose

up:
	sudo chmod 777 -R docker
	$(compose) up -d --build
	docker image prune -f
	docker volume prune -f

down:
	$(compose) down -v --remove-orphans

restart: down up

console:
	cd ConsoleClient
	docker run -it --rm --network webtest_backend webtest_console /bin/sh -c "./ConsoleClient"
