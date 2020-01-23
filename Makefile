all: pack publish
pack:
	./scripts/pack.sh 
publish:
	./scripts/publish.sh
bump:
	./scripts/bump.sh

	