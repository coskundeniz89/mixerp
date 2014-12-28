CREATE VIEW core.account_view
AS
SELECT
    core.accounts.account_id,
    core.accounts.account_number || ' (' || core.accounts.account_name || ')' AS account,
    core.accounts.account_number,
    core.accounts.account_name,
    core.accounts.description,
    core.accounts.external_code,
    core.accounts.currency_code,
    core.accounts.confidential,
    core.accounts.is_cash,
    core.accounts.is_employee,
    core.accounts.is_party,
    core.accounts.normally_debit,
    core.accounts.sys_type,
    core.accounts.parent_account_id,
    parent_accounts.account_number AS parent_account_number,
    parent_accounts.account_name AS parent_account_name,
    parent_accounts.account_number || ' (' || parent_accounts.account_name || ')' AS parent_account,
    core.account_masters.account_master_id,
    core.account_masters.account_master_code,
    core.account_masters.account_master_name,
    core.has_child_accounts(core.accounts.account_id) AS has_child,
    core.cash_flow_headings.cash_flow_heading_code,
    core.cash_flow_headings.cash_flow_heading_name
FROM
    core.account_masters
    INNER JOIN core.accounts 
    ON core.account_masters.account_master_id = core.accounts.account_master_id
    LEFT OUTER JOIN core.accounts AS parent_accounts 
    ON core.accounts.parent_account_id = parent_accounts.account_id
    LEFT OUTER JOIN core.cash_flow_headings
    ON core.accounts.cash_flow_heading_id = core.cash_flow_headings.cash_flow_heading_id;