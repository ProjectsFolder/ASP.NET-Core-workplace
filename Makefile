compose=docker-compose

up:
	$(compose) up -d --build
	docker image prune -f
	docker volume prune -f

down:
	$(compose) down -v --remove-orphans

restart: down up
