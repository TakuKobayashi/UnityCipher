# AI release notes composite action

Creates or updates a GitHub Release by summarizing the Git history and file diff
between the current semantic-version tag and its previous reachable release tag.
Inference runs locally with Ollama, so source diffs are not sent to a hosted LLM
and no model API key is required. On a GitHub-hosted Linux runner, the action
installs and starts Ollama when it is not already available.

```yaml
permissions:
  contents: write

steps:
  - uses: actions/checkout@v6
    with:
      fetch-depth: 0
  - uses: ./.github/actions/ai-release-notes
    with:
      github-token: ${{ github.token }}
      tag: ${{ github.ref_name }}
      model: qwen2.5-coder:7b-instruct
      language: jp
      bilingual: 'true'
```

The action gives commit subjects, authors, a diff summary, and up to 30,000
characters of eligible text patches to Ollama in one request. The patch budget
is shared across changed text files so later files are not silently omitted.
The default `qwen2.5-coder:7b-instruct`
model is code-focused, fits comfortably on a standard GitHub-hosted runner,
and uses a 16K context window by default. The smaller input defaults avoid
excessively long prompt evaluation while retaining the commit and file summary.
If Ollama produces no network activity for 600 seconds, the request stops and
the action publishes
deterministic notes containing the commit list and diff summary. Set
`fail-on-llm-error: "true"` to disable that fallback. Use `ollama-host` to point
at an existing Ollama server. `max-diff-chars`, `num-ctx`, and
`inference-timeout-seconds` can be adjusted for the runner hardware.

Non-source and binary contents are excluded from the patch sent to Ollama. This
includes image, video, audio and 3D files; archives and packages such as
`unitypackage`; executables and libraries; fonts and binary office documents;
databases and datasets; ML weights; game-engine bundles; source maps; and
credential containers. Their paths, change statuses, and diff statistics remain
available as supporting context. When a release changes only excluded files,
the action tells the model to derive the summary primarily from commit messages
rather than pretending to inspect their contents. Lockfiles and ordinary text
configuration such as JSON and YAML remain visible in the file summary, but
lockfile contents, Unity `.meta` files, editor layouts, and `UserSettings` are
not sent as patch content. While Ollama is evaluating the prompt, the action logs
elapsed time every 15 seconds. Once generation starts, it logs elapsed time and
the number of response characters received.

`language` defaults to `en` and produces notes only in the selected language.
Set `bilingual: "true"` to include English first and the selected language
second. Both `ja` (the standard language code) and `jp` are accepted for
Japanese; this repository passes `jp` with bilingual mode enabled.

Outputs are `release-url`, `previous-tag`, and `used-llm`.

## Local preview

Start Ollama and pull the configured model, then run the script from the
repository root. In dry-run mode, `tag` defaults to the current `HEAD`, no
`GITHUB_TOKEN` is needed, and no GitHub Release is changed.

The local preview assumes that Ollama is already installed and its server is
running. Before reading Git history, the script checks the server and configured
model. If either is unavailable, it exits with a concrete `ollama serve` or
`ollama pull <model>` command instead of a generic inference error.

```powershell
# Run `ollama serve` in another terminal first if Ollama is not already running.
ollama pull qwen2.5-coder:7b-instruct
node .github/actions/ai-release-notes/generate-release-notes.mjs `
  --dry-run `
  --language jp `
  --bilingual `
  --model qwen2.5-coder:7b-instruct `
  --output-file release-notes-preview.md
```

Pass `--tag v1.2.3` to preview an existing tag instead of `HEAD`. Run the script
with `--help` for all options. The `INPUT_*` environment variables remain
supported because the composite action uses them internally.

To publish this in GitHub Marketplace later, move this directory into a public,
dedicated repository so that `action.yml` is at its root, then replace the local
`uses` path with `OWNER/ACTION@v1` (or preferably a full commit SHA).
