#!/usr/bin/env bash
# Creates the starter labels and issues for the Advanced GitHub Copilot course in your copy of Globomantics Shipping.
#
# Usage:
#   ./setup/create-issues.sh                      # uses the repository of the current clone
#   ./setup/create-issues.sh your-name/globomantics-shipping
#
# Needs the GitHub CLI (gh) and jq. It's safe to run more than once:
# issues that already exist (same title) are skipped.
set -euo pipefail

command -v gh >/dev/null || { echo 'The GitHub CLI (gh) is required. Install it from https://cli.github.com and run "gh auth login".' >&2; exit 1; }
command -v jq >/dev/null || { echo 'jq is required. Install it from https://jqlang.org.' >&2; exit 1; }
gh auth status >/dev/null 2>&1 || { echo 'You are not signed in to the GitHub CLI. Run "gh auth login" and try again.' >&2; exit 1; }

repo="${1:-$(gh repo view --json nameWithOwner --jq .nameWithOwner)}"
setup_dir="$(cd "$(dirname "$0")" && pwd)"

echo "Setting up issues in $repo"

# Forks have issues turned off by default.
if [[ "$(gh repo view "$repo" --json hasIssuesEnabled --jq .hasIssuesEnabled)" != "true" ]]; then
  echo "Turning on issues for the repository"
  gh repo edit "$repo" --enable-issues >/dev/null
fi

jq -c '.[]' "$setup_dir/labels.json" | while read -r label; do
  name="$(jq -r .name <<<"$label")"
  gh label create "$name" --repo "$repo" \
    --color "$(jq -r .color <<<"$label")" \
    --description "$(jq -r .description <<<"$label")" \
    --force >/dev/null </dev/null
  echo "  label   $name"
done

existing="$(gh issue list --repo "$repo" --state all --limit 500 --json title --jq '.[].title')"

jq -c '.[]' "$setup_dir/issues.json" | while read -r issue; do
  title="$(jq -r .title <<<"$issue")"

  if grep -Fxq "$title" <<<"$existing"; then
    echo "  skipped $title (already exists)"
    continue
  fi

  args=(issue create --repo "$repo" --title "$title" --body-file "$setup_dir/$(jq -r .body <<<"$issue")")
  while read -r label; do
    args+=(--label "$label")
  done < <(jq -r '.labels[]' <<<"$issue")

  url="$(gh "${args[@]}" </dev/null)"
  echo "  created $title $url"
done

echo "Done."
