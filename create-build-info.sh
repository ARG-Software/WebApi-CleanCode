#!/bin/bash

workspace=$1		# Comes from $BITBUCKET_REPO_OWNER
user=$2             # User for BitBucket  
repository=$4		# Comes from $BITBUCKET_REPO_SLUG
buildNumber=$5		# Comes form $BITBUCKET_BUILD_NUMBER
commitHash=$6 		# Comes from $BITBUCKET_COMMIT
gitBranch=$7		# Comes from $BITBUCKET_BRANCH
gitOrigin=$8		# Comes from $BITBUCKET_GIT_HTTP_ORIGIN
buildUrl="https://bitbucket.org/$workspace/$repository/addon/pipelines/home#!/results/$buildNumber"

pass="tGgmxRwSF6zVVtFSW6DQ" # User password

echo "Workspace: $workspace"
echo "User: $user"
echo "Password: $pass"
echo "Repository: $repository"
echo "BuildNumber: $buildNumber"
echo "CommitHash: $commitHash"
echo "GitBranch: $gitBranch"
echo "GitOrigin: $gitOrigin"
echo "BuildUrl: $buildUrl"

# Get Git commit info
if [ $buildNumber -eq 1 ] 
	then
	echo "Getting commits for $commitHash"
	git log --pretty=oneline $commitHash -1 > git-commits.log
else
	previousbuildNumber=$(expr $buildNumber - 1)
	baseURL="https://api.bitbucket.org/2.0/repositories/$workspace/$repository/pipelines"
	
	prevCommitHashURL="$baseURL/$previousbuildNumber"
	echo "$prevCommitHashURL"
	echo "$user"
	echo "$pass"

	prevFullHash=$(curl -X GET "$prevCommitHashURL" -u "$user":"$pass" | jq  '.target.commit.hash')
	echo $prevFullHash

	if [ -z "$prevFullHash"]
		then
		echo "Commit Hash not set"
		exit 1
	fi

	prevHash="${prevFullHash%\"}"
	prevHash="${prevFullHash#\"}"
	commitHash="${commitHash%\"}"
	commitHash="${commitHash#\"}"
	
	prevHash=$(echo $prevHash | cut -b 1-7)
	commitHash=$(echo $commitHash | cut -b 1-7)

	
	if [ $prevHash == $commitHash ] 
		then
		echo "Getting commits for $commitHash"
		git log --pretty=oneline $commitHash -1 > git-commits.log
	else
		echo "Comparing between $prevHash and $commitHash"
		git log --pretty=oneline "$prevHash".."$commitHash" > git-commits.log
	fi
fi

echo "Building commit json"

# Create Commits Json
commits=''
counter=0
while read l; do
	hash=$(echo "$l" | cut -d' ' -f1)
	msg=$(echo "$l" | cut -d' ' -f 2-)
	linkUrl="$gitOrigin/commits/$hash"
	commit=$(jq -n \
		--arg id "$hash" \
		--arg url "$linkUrl" \
		--arg msg "$msg" \
		'{Id: $id, LinkUrl: $url, Comment: $msg}' \
		)
		
	if [ $counter -eq 0 ]
		then
		commits="$commit"
		else
		commits="$commits,$commit"
	fi
	counter=$(expr $counter + 1)
done < git-commits.log

# Delete temporary file
echo "Deleting temporary git-commits.log file"
rm git-commits.log

echo "Creating build info"
buildInfo=$(jq -n \
		--arg be "BitBucket" \
		--arg br "$gitBranch" \
		--arg bn "$buildNumber" \
		--arg bu "$buildUrl" \
		--arg vcs "Git" \
		--arg vcr "$gitOrigin" \
		--arg vcn "$commitHash" \
		--argjson cmt "[$commits]" \
		'{BuildEnvironment: $be, Branch: $br, BuildNumber: $bn, BuildUrl: $bu, VcsType: $vcs, VcsRoot: $vcr, VcsCommitNumber: $vcn, Commits: $cmt}' \
		)		
echo "Writing out octopus.buildinfo file"
echo $buildInfo
echo $buildInfo > octopus.buildinfo