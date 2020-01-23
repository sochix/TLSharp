#!/usr/bin/env bash

VERSION=`cat ./VERSION`
TAG=`echo "$VERSION" | awk -F. -v OFS=. 'NF==1{print ++$NF}; NF>1{if(length($NF+1)>length($NF))$(NF-1)++; $NF=sprintf("%0*d", length($NF), ($NF+1)%(10^length($NF))); print}'`

# git -c core.quotepath=false checkout -b "release/$TAG"
echo "$TAG" > ./VERSION
# git -c core.quotepath=false add --ignore-errors ./VERSION
# git -c core.quotepath=false commit -am "$TAG"
# git checkout develop
# git merge --no-ff --no-edit "release/$TAG"
# git checkout master
# git merge --no-ff --no-edit "release/$TAG"
# git tag $TAG
# git push origin master develop
# git push --tags origin
# git checkout develop
# git branch -d "release/$TAG"

exit 0
