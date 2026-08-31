#!/usr/bin/env bash
# Regenerates the Kiota client from the live Partner API OpenAPI description.
#
# Run this deliberately when you want to adopt API changes — NOT on every build. It rewrites Generated/;
# review the diff (git diff Generated/) and commit it so the change is visible and reproducible.
#
# Requires the Kiota CLI:  dotnet tool install --global Microsoft.OpenApi.Kiota
set -euo pipefail

cd "$(dirname "$0")"

OPENAPI_URL="${OPENAPI_URL:-https://connect.hypelabs.network/openapi/v1.json}"

echo "Regenerating client from ${OPENAPI_URL} …"
kiota generate \
  --language CSharp \
  --openapi "${OPENAPI_URL}" \
  --class-name PartnerApiClient \
  --namespace-name HypeLabs.Partner.Sdk.Generated \
  --output ./Generated \
  --clean-output

echo "Done. Review with:  git diff Generated/"
